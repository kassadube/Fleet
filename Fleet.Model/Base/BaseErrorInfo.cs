using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolkit.Serialization;

namespace Fleet.Model
{
    [TSerialize("Error", ElementType.Element, TryAll = true)]
    [TDataSerialize(TryAll = true)]
    public class BaseErrorInfo
    {
        public string Name { get; set; }

        public long Code { get; set; }

        public int Severity { get; set; }

        public int ReturnValue { get; set; }

        [TSerialize(Ignore = true)]
        private Exception EX { get; set; }

        [TSerialize("Msg", ElementType.CData)]
        public string Message { get; set; }

        public void BuildException(Exception ex)
        {
            EX = ex;
            string expMessage = "Message: {0} Source: {1} StackTrace: {2}; Description : {3} ErrorCode: {4} Source: Custom StackTrace: None;";
            Message = string.Format(expMessage, ex.Message, ex.Source, ex.StackTrace, Name, Code.ToString());
           
        }
    }
}
