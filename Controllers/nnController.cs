using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webproject2.Models;
using Accord.Neuro;
using Accord.Neuro.Learning;
using Accord.Neuro.Networks;
using Accord.Math;
using webproject2.Helper;
using Accord.Neuro.ActivationFunctions;
using webproject2.Model.repositories;
using webproject2.Model;
using Microsoft.AspNetCore.Hosting;
using System.IO; 
using Microsoft.AspNetCore.Http;


namespace webproject2.Controllers
{
    public class nnController : Controller
    {
        private readonly ILogger<nnController> _logger;
        private UnitOfWork unitOfWork = new UnitOfWork(); 
        public nnController(ILogger<nnController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Dbn> res=unitOfWork.dbnrepository.GetAll().ToList();
            return View(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult buildnn(int? id)
        {
            if (id >0)
            {
                Dbn _dbn=unitOfWork.dbnrepository.GetById(id);
                return View(_dbn);
            }
            return View(new Dbn());
        }

        public IActionResult trainingsetting(int? id)
        {
            if (id >0)
            {
                Dbn _dbn=unitOfWork.dbnrepository.GetById(id);
                return View(_dbn);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult predictions()
        {
            List<Prediction> res=unitOfWork.predictionrepository.GetAll().ToList();
            return View(res);
        }
        public IActionResult predict(int? id)
        {
             ViewBag.listdbn = unitOfWork.dbnrepository.GetAll().Where(x=> x.Trainingset !=null && x.Trainingset==true).ToList();
            if (id >0)
            {
                Prediction _Prediction=unitOfWork.predictionrepository.GetById(id);
                return View(_Prediction);
            }     
            return View(new Prediction());
        }
       [HttpPost]
       public IActionResult  adddbn(Dbn _Dbn){
            
            if(_Dbn.Id==0){
                //insert
                 unitOfWork.dbnrepository.Add(_Dbn);
                 unitOfWork.Commit();    
                            
            }else{
                //update
                Dbn _dbnDB=unitOfWork.dbnrepository.GetById(_Dbn.Id);
                if(_dbnDB!=null){
                _dbnDB.Dbnname=_Dbn.Dbnname;
                _dbnDB.Inputneuronscount=_Dbn.Inputneuronscount;
                _dbnDB.Outputneuronscount=_Dbn.Outputneuronscount;
                _dbnDB.Hiddenlayerscount=_Dbn.Hiddenlayerscount;
                _dbnDB.Hiddenneuronscount=_Dbn.Hiddenneuronscount;
                unitOfWork.dbnrepository.Update(_dbnDB,_dbnDB.Id);
                unitOfWork.Commit();
                }
            }
            return RedirectToAction(nameof(Index));

       }  

       [HttpPost]
       public IActionResult  trainingsettingadd(Dbn _Dbn){
            if(_Dbn!=null){
            if(_Dbn.Id>0){
                Dbn _dbnDB=unitOfWork.dbnrepository.GetById(_Dbn.Id);
                if(_dbnDB!=null){
                _dbnDB.Learningrate=_Dbn.Learningrate;
                _dbnDB.Datasetsize=_Dbn.Datasetsize;
                _dbnDB.NumoftrainingEpoch=_Dbn.NumoftrainingEpoch;
                _dbnDB.Trainingset=true;
                if(Request.Form.Files!=null && Request.Form.Files.Count>0){
                  _dbnDB.Datasetfilename= UploadedFile();               
                } 
                //update
                unitOfWork.dbnrepository.Update(_dbnDB,_dbnDB.Id);
                unitOfWork.Commit(); 
                }                       
            }
            }
            return RedirectToAction(nameof(Index));

       }
      
        public ActionResult DownloadFile()
        {
            string fname = HttpContext.Request.Query["fname"].ToString();
            string filepath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "data",fname);
            // OPTIONAL - validation to check if it is allowed to download this file.
            //string filetype = Helpers.GetMimeType(filepath);
            return File(filepath, "text/plain", Path.GetFileName(filepath));
        }
       private string UploadedFile()  
        {  
            string uniqueFileName = null;  
            var files = Request.Form.Files; 
            foreach(var file in files){ 
            if (file!=null && file.Length>0 )  
            {  
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "data");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    file.CopyTo(fileStream);  
                }  
            } 
            break; 
            }
            return uniqueFileName;  
        }         
       [HttpGet]
       public int  DeleteDbn(int? id){
            
            if(id>0){
                //insert
                 unitOfWork.dbnrepository.Delete(id);
                 unitOfWork.Commit();    
                            
            }
            return 1;

       }    
       //DeletePrediction   
       [HttpGet]
       public int  DeletePrediction(int? id){       
            if(id>0){
                //insert
                 unitOfWork.predictionrepository.Delete(id);
                 unitOfWork.Commit();                 
            }
            return 1;
       }  
        //predict

   


        [HttpPost]
       public int  predictone(predictioninputsviewmodel _predictioninputsviewmodel){
            Dbn _dbnDB=unitOfWork.dbnrepository.GetById(_predictioninputsviewmodel.dbnid); 
            if(_dbnDB!=null){
            int inputsCount=_dbnDB.Inputneuronscount.Value;
            int outputsCount=_dbnDB.Outputneuronscount.Value;
            int hiddenNeurons=_dbnDB.Hiddenneuronscount.Value;
            int hiddenLayersCount=_dbnDB.Hiddenlayerscount.Value;
            int datasetsize=_dbnDB.Datasetsize.Value;
            double LearningRate=Double.Parse(_dbnDB.Learningrate.Value.ToString());
            int numoftrainingEpoch=_dbnDB.NumoftrainingEpoch.Value;
            string trainingfilename=_dbnDB.Datasetfilename;
            double[,] outputValues=new double[datasetsize,outputsCount];
            double[] userinputs={_predictioninputsviewmodel.buildingarea
            ,_predictioninputsviewmodel.floorheight,_predictioninputsviewmodel.nooffloores
            ,_predictioninputsviewmodel.envelopetype,_predictioninputsviewmodel.yearofbuilt
            ,_predictioninputsviewmodel.buildingage};
                   
            //loading dataset for training
            double[,] inputs=new double[datasetsize,inputsCount];
            double[,] outputs=new double[datasetsize,outputsCount];
            new filemangerhelp().readfiledt(trainingfilename,inputsCount,outputsCount,inputs,outputs,',');
            //normalize inputs and outputs
            double[] maxinputs=new double[inputs.Columns()];
            double[] maxoutputs=new double[outputs.Columns()];
            
            for (int x = 0; x < maxinputs.Length; x++)
            {
                maxinputs[x]=inputs.GetColumn(x).Max();
            }          
            for (int z = 0; z < maxoutputs.Length; z++)
            {
                maxoutputs[z]=outputs.GetColumn(z).Max();
            }            
            for (int i = 0; i < inputs.Rows(); i++)
            {
                for (int y = 0; y < inputs.Columns(); y++)
                {
                    inputs[i,y]=(inputs[i,y])/(maxinputs[y]);
                }
            }
            for (int i = 0; i < outputs.Rows(); i++)
            {
                for (int y = 0; y < outputs.Columns(); y++)
                {
                    outputs[i,y]=(outputs[i,y])/(maxoutputs[y]);
                }
            }
            //normalize user inputs
            for (int y = 0; y < userinputs.Length; y++)
            {
                userinputs[y]=(userinputs[y])/(maxinputs[y]);
            }

            //initialize the network and set random weights     
            int[] arr=new int[hiddenLayersCount+1];
            for (int i = 0; i < hiddenLayersCount+1; i++)
            {
                if(i==hiddenLayersCount){
                  arr[i]=outputsCount; 
                }else{
                  arr[i]=hiddenNeurons;
                } 
            }
            DeepBeliefNetwork network = new DeepBeliefNetwork( inputsCount,arr);        
            new GaussianWeights(network, 0.1).Randomize();
            network.UpdateVisibleWeights();
             // Supervised learning on entire network, to provide output classification.
            var teacher2 = new BackPropagationLearning(network)
            {
                LearningRate = LearningRate,
                Momentum = 0.5
            };
            // Run supervised learning.
            double error=0;
            for (int i = 0; i < numoftrainingEpoch; i++)
            {
               error= teacher2.RunEpoch(inputs.ToJagged(), outputs.ToJagged()) ;  
            }
            // predict the and get the output
            double[]  outputValuesitem = network.Compute(userinputs);
            for (int y = 0; y < outputValuesitem.Length; y++)
            {
                //denormalize the value
                outputValuesitem[y]=outputValuesitem[y]*maxoutputs[y];
            }
            outputviewmodel _outputviewmodel=formatoutput(outputValuesitem);
            _outputviewmodel.errorratio=Math.Round(error,2);
            
            Prediction _Prediction=new Prediction();
            _Prediction.Projectname =_predictioninputsviewmodel.projectname;
            _Prediction.Dbnid =_predictioninputsviewmodel.dbnid;
            _Prediction.Buildingarea =Decimal.Parse(_predictioninputsviewmodel.buildingarea.ToString());
            _Prediction.Floorheight =Decimal.Parse(_predictioninputsviewmodel.floorheight.ToString());
            _Prediction.Nooffloores =Decimal.Parse(_predictioninputsviewmodel.nooffloores.ToString());
            _Prediction.Envelopetype =Decimal.Parse(_predictioninputsviewmodel.envelopetype.ToString());
            _Prediction.City =Decimal.Parse(_predictioninputsviewmodel.city.ToString());
            _Prediction.Yearofbuilt =Decimal.Parse(_predictioninputsviewmodel.yearofbuilt.ToString());
            _Prediction.Buildingage =Decimal.Parse(_predictioninputsviewmodel.buildingage.ToString());
            _Prediction.Createdat =DateTime.Now;
            _Prediction.Initialcost =Decimal.Parse(_outputviewmodel.Initialcost.ToString());
            _Prediction.Energycost =Decimal.Parse(_outputviewmodel.Energycost.ToString());
            _Prediction.Cateringandservices =Decimal.Parse(_outputviewmodel.Cateringandservices.ToString());
            _Prediction.Cleaning =Decimal.Parse(_outputviewmodel.Cleaning.ToString());
            _Prediction.Majorrepairs =Decimal.Parse(_outputviewmodel.Majorrepairs.ToString());
            _Prediction.PeriodicMaintena =Decimal.Parse(_outputviewmodel.PeriodicMaintena.ToString());
            _Prediction.RentandInsurances =Decimal.Parse(_outputviewmodel.RentandInsurances.ToString());
            _Prediction.OperatingandMaintenance =Decimal.Parse(_outputviewmodel.OperatingandMaintenance.ToString());
            _Prediction.Structureandenvelopematerial =Decimal.Parse(_outputviewmodel.Structureandenvelopematerial.ToString());
            _Prediction.MarketpriceofresultedCo2 =Decimal.Parse(_outputviewmodel.MarketpriceofresultedCO2.ToString());
            _Prediction.Environmentalcost =Decimal.Parse(_outputviewmodel.Environmentalcost.ToString());
            _Prediction.Salvagevalue =Decimal.Parse(_outputviewmodel.Salvagevalue.ToString());
            _Prediction.Demolitioncost =Decimal.Parse(_outputviewmodel.Demolitioncost.ToString());
            _Prediction.Endoflifecost =Decimal.Parse(_outputviewmodel.Endoflifecost.ToString());
            _Prediction.Totallcccost =Decimal.Parse(_outputviewmodel.TOTALlcccost.ToString());           
            _Prediction.Errorratio=Decimal.Parse(_outputviewmodel.errorratio.ToString());
            unitOfWork.predictionrepository.Add(_Prediction);
            unitOfWork.Commit();
            return  _Prediction.Id;
            }
            return 0;
       }

           
      
       private outputviewmodel formatoutput(double[] dnnoutput)  {
         //lenght 11
         outputviewmodel _outputviewmodel=new outputviewmodel();
        _outputviewmodel.Initialcost =Math.Round(dnnoutput[0],2);      
        _outputviewmodel.Energycost  =Math.Round(dnnoutput[1]);
        _outputviewmodel.Cateringandservices =Math.Round(dnnoutput[2]);
        _outputviewmodel.Cleaning  =Math.Round(dnnoutput[3]);
        _outputviewmodel.Majorrepairs  =Math.Round(dnnoutput[4]);
        _outputviewmodel.PeriodicMaintena =Math.Round(dnnoutput[5]);
        _outputviewmodel.RentandInsurances  =Math.Round(dnnoutput[6]);       
        _outputviewmodel.OperatingandMaintenance  =Math.Round(dnnoutput[1]+dnnoutput[2]+dnnoutput[3]+dnnoutput[4]+dnnoutput[5]+dnnoutput[6]);
        _outputviewmodel.Structureandenvelopematerial  =Math.Round(dnnoutput[7]);        
        _outputviewmodel.MarketpriceofresultedCO2   =Math.Round(dnnoutput[8]);
        _outputviewmodel.Environmentalcost  =Math.Round(dnnoutput[7]+dnnoutput[8]);
        _outputviewmodel.Salvagevalue =Math.Round(dnnoutput[9]);
        _outputviewmodel.Demolitioncost    =Math.Round(dnnoutput[10]);
        _outputviewmodel.Endoflifecost  =Math.Round(dnnoutput[10]-dnnoutput[9]);
        _outputviewmodel.TOTALlcccost =Math.Round(_outputviewmodel.Initialcost+_outputviewmodel.OperatingandMaintenance+_outputviewmodel.Environmentalcost+_outputviewmodel.Endoflifecost);
        return _outputviewmodel;
       }
       

       
    }
}
