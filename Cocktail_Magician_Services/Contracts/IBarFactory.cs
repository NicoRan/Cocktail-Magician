using Cocktail_Magician_DB.Models;
using System;

namespace Cocktail_Magician_Services.Contracts
{
    public interface IBarFactory
    {
        Bar CreateNewBar(string name, string address, string information, string picture, string mapDirection);
        BarReview CreateNewBarReview(double grade, string comment, string userId, string barId, DateTime createdOn);
    }
}