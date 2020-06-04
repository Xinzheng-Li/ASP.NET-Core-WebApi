using System;
using System.Xml;

namespace WebApi.Utilities
{
    public static class XMLHelper
    {
        /// <summary>
        /// Get XmlNode By XPATH
        /// </summary>
        /// <param name="xmlFileName"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(xmlFileName);
                XmlNode xmlNode = xmlDoc.SelectSingleNode(xpath);
                return xmlNode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
