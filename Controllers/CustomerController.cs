using System.Collections.Generic;
using FoodPortal.API.Core.Entity;
using FoodPortal.API.Core.Repository.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

[ApiController]
[Route("Customer")]
public class CustomerController:ControllerBase
{
    private IUnitOfWork iuow = null;

    public CustomerController(IUnitOfWork iuow)
    {
        this.iuow = iuow;
    }
    public ActionResult<List<Customer>> Index()
    {
        return iuow.CustRepository.GetAll().ToList();
    }

    public ActionResult<Customer> GetByID(int id)
    {
        Customer obj = iuow.CustRepository.GetFiltered(c=>c.CID==id).FirstOrDefault();
        if(obj!=null){
            return obj;
        }
        else{
            return Content("Customer data not found...........");
        }
    }

    public ActionResult CheckEmail(string email)
    {
        Customer obj = iuow.CustRepository.GetFiltered(c=>c.EMail==email).FirstOrDefault();
        if(obj != null){
            return BadRequest("Already registerd with given email id........");            
        }
        else{
            return Content("");
        }
    }
    public ActionResult Register(Customer obj)
    {

        try
        {
            if(TryValidateModel(obj))
            {
                Customer c = iuow.CustRepository.GetFiltered(c=>c.Mobile==obj.Mobile).FirstOrDefault();
                if(c==null)
                {
                    throw new Exception("Already Registerd with the given mobile number.");
                }
                
                iuow.CustRepository.Add(obj);
                iuow.Complete();
                return Content("Customer Registered Successfully...........");
            }
            else{
                return BadRequest(ModelState);
            }
            
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    public ActionResult Login(string email,string password)
    {
        Customer c = iuow.CustRepository.GetFiltered(c=>((c.EMail == email)&&c.Password == password)).FirstOrDefault();
        if(c != null){
            return Content("SUCCESS");            
        }
        else{
            return BadRequest("Provided email and password is invalid...........");
        }
    }

    public ActionResult Logout()
    {
        return Content("Logged out successfully.");
    }

    public ActionResult Edit(Customer obj){
        try
        {
            if(TryValidateModel(obj))
            {                
                iuow.CustRepository.Update(obj);
                iuow.Complete();
                return Content("Customer data modified Successfully...........");
            }
            else{
                return BadRequest(ModelState);
            }
            
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    public ActionResult Delete(int id){
        try
        {              
            iuow.CustRepository.Delete(id);
            iuow.Complete();
            return Content("Customer data deleted Successfully...........");
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }
}