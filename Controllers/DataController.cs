﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using System.Net.Http;
using System.Net;
using System.Text;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreAPI.Controllers
{
    [Route("[controller]/[Action]")]
    public class DataController : Controller
    {
        private static readonly ShopDBContext db = new ShopDBContext();

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectUser()
        {
            try
            {
                List<User> list = db.User.ToList();
                return Ok(Json(list));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(User u)
        {
            try
            {
                u.RegisterTime = DateTime.Now;
                u.LastLoginTime = null;
                u.AddressId = null;
                db.User.Add(u); //对表进行添加操作
                int row = db.SaveChanges();
                if (row > 0)
                {
                    return Ok();
                }
                else
                {
                   return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public  ActionResult Login(string userName, string pwd)
        {
            User us = null;
            try
            {
                User u = db.User.ToList().Where(n => (n.LoginName == userName || n.Email == userName || n.Phone == userName) && n.PassWord == pwd).SingleOrDefault();
                if (u != null)
                {
                    us = u;
                    u.LastLoginTime = DateTime.Now;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return NotFound();
            }
            

        }
    }
}
