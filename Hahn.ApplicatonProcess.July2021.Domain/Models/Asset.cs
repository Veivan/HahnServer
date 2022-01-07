using System.ComponentModel.DataAnnotations;

namespace Hahn.ApplicatonProcess.July2021.Domain.Models
{
    public class Asset
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
    }
}
