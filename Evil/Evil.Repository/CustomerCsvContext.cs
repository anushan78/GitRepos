using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Evil.Entity;

namespace Evil.Repository
{
    public class CustomerCsvContext : IDataContext<Customer>
    {
        public CustomerCsvContext(string fileKey)
        {
            FileKey = fileKey;
        }

        private string FileKey { get; set; }

        public List<Customer> Retrieve()
        {
            var path = ConfigurationManager.AppSettings[FileKey];
            var csvLines = File.ReadAllLines(path);

            if (csvLines.Length < 0)
                throw new Exception("No Data Available");

            var val = csvLines
                .Where(a => !string.IsNullOrEmpty(a))
                .Select(line => line.Split(','))
                .Select(tokens => new Customer {Name = tokens[0], Value = Convert.ToInt32(tokens[1].Trim())})
                .ToList();

            return val;
        }
    }
}