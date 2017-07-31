using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Utills.PMSExceptions
{
    public class RecordNotFoundException : PMSException
    {
        public RecordNotFoundException() : base("Record not found") { }

        public RecordNotFoundException(string entity, int id) : base(string.Format("{0} (Id: {1}) is not found", entity, id)) { }
    }
}
