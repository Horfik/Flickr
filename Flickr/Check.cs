using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report
{
    class Check
    {
        public bool ElementsConteinAttribute(IList<IWebElement> element, string attribyte)
        {

            for (int i = 0; i < element.Count(); i++)
            {
                if (element[i].GetAttribute(attribyte) != null)
                {
                    ;
                }
                else return false;
            }
            return true;
        }

        public bool Elements(IList<IWebElement> element)
        {

            for (int i = 0; i < element.Count(); i++)
            {
                if (element[i] != null)
                {
                    ;
                }
                else return false;
            }
            return true;
        }
    }
}
