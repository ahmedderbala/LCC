            
using System.IO;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;
namespace webproject2.Helper
{
    public  class filemangerhelp
    {
        public filemangerhelp(){
            
        }
        public void readfiledt(string filename,int inputLength,int outputLength,double[,] inputs,double[,] outputs,char delim){
            var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", filename );
            using (System.IO.TextReader tr = File.OpenText(file))
            {
                string line;
                int i=0;
                while ((line = tr.ReadLine()) != null)
                {                       
                   string[] items = line.Trim().Split(delim); 
                   for (int j = 0; j <inputLength; j++)
                   {
                       inputs[i,j]=Double.Parse(items[j]);
                   }
                   for (int j = inputLength; j < inputLength+outputLength; j++)
                   {
                       outputs[i,j-inputLength]=Double.Parse(items[j]);
                   }
                   i++;
                }
            }

        }

        public  double normalizeinput(double x){
			if(x<=1){
				return x;
			}
			else if(x<=10){
				return x/10;
			}else if(x<=50){
                 return  x/50;
			}else if (x<=200){
				return x/200;
			}else return x;
		}
        
    }
}