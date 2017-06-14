using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.Serialization;
using System.Xml.Linq;
using System.Collections.Specialized;

namespace Fleet.Model
{
    [TSerialize("Result", ElementType.Element, TryAll = true)]
    public class BaseResultInfo
    {
        [TSerialize("Error", ElementType.Element, IsNested = true)]
        public BaseErrorInfo Error { get; set; }

        public long TokenId { get; set; }

        [TSerialize(Ignore = true)]
        public int ReturnValue { get; set; }

        [TSerialize("ReturnValue", ElementType.Element, DefaultValue = "false")]
        public bool ResultValue { get { return ReturnValue == 0 ? false : true; } set { ReturnValue = value ? 1 : 0; } }

        [TSerialize(Ignore = true)]
        public bool IsSucceded { get { return Error == null; } }

        public virtual object ResultObject { get; set; }
       
        public T GetResult<T>()  where T : class
        {
            return ResultObject as T;
        }

        public bool TryGetResult<T>(out T ToItem) where T : class
        {
            try
            {
                ToItem = ResultObject as T;
            }
            catch (Exception)
            {
                ToItem = null;
                return false;
            }

            return true;
        }

        [TDataSerialize(Ignore = true)]
        [TSerialize(Ignore = true)]
        public NameValueCollection Params { get; set; }

        public virtual XElement ToXElement()
        {
            XElement res = new XElement("Result");
            res.Add(new XElement("IsSucceded", this.IsSucceded.ToString()));
            //res.Add(new XElement("ReturnValue", this.ReturnValue.ToString()));
            if (Error != null)
            {
                XElement x = new XElement("ResultObject");
                x.Add(Serializer.Serialize(Error));
                res.Add(x);
            }
            return res;
        }
    }
}
