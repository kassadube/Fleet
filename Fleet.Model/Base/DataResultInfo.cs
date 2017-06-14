using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Toolkit.Serialization;

namespace Fleet.Model
{
	public class DataResultInfo<T> : BaseResultInfo where T : class
	{
        public new T ResultObject { get { return base.ResultObject as T; } set { base.ResultObject = value; } }

        public override XElement ToXElement()
        {
            XElement res = base.ToXElement();
            if (Error != null) return res;

            XElement x = new XElement("ResultObject");
            if (ResultObject.GetType() == typeof(XElement))
            {
                x.Add(ResultObject);
            }
            else
            {
                x.Add(Serializer.Serialize(ResultObject));
            }

            res.Add(x);
            return res;
        }
	}
}

