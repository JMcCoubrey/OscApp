using System.Collections.Generic;

namespace OscApp.Model.DataViewModels
{
    public class GenericDataViewModel<T>
    {
        public ICollection<T> Models { get; set; }
        public string SearchTerm { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int TotalNumResults { get; set; }
        public int NumPages { get; set; }
        public int CurrentPageNumber { get; set; }
    }
}
