﻿using System.IO;
using System.Text;

namespace xCarpaccio.client
{
    using Nancy;
    using System;
    using Nancy.ModelBinding;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => "It works !!! You need to register your server on main server.";

            Post["/order"] = _ =>
            {
                try
                {
                    using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                    {
                        Console.WriteLine("Order received: {0}", reader.ReadToEnd());
                    }

                    // Order loading
                    var order = this.Bind<Order>();
                    // Ca
                    var bill = new BillCalculator().Calculate(order);

                    //TODO: do something with order and return a bill if possible

                    // If you manage to get the result, return a Bill object (JSON serialization is done automagically)
                    // Else return a HTTP 404 error : return Negotiate.WithStatusCode(HttpStatusCode.NotFound);
                    return bill;
                }
                catch (Exception)
                {
                    return null;
                }
            };

            Post["/feedback"] = _ =>
            {
                var feedback = this.Bind<Feedback>();
                Console.Write("Type: {0}: ", feedback.Type);
                Console.WriteLine(feedback.Content);
                return Negotiate.WithStatusCode(HttpStatusCode.OK);
            };
        }
    }
}