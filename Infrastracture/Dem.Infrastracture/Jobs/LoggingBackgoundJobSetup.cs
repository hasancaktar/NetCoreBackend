﻿using Microsoft.Extensions.Options;
using Quartz;

namespace Dem.Infrastracture.Jobs;

public class LoggingBackgoundJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = JobKey.Create(nameof(LoggingBackgoundJob)); //bir işin benzersiz bir anahtarını oluşturur. Bu anahtar, LoggingBackgoundJob işini tanımlar.
        options.AddJob<LoggingBackgoundJob>(jobBuilder => jobBuilder
        .WithIdentity(jobKey)) //LoggingBackgoundJob işini QuartzOptions'a ekler. .WithIdentity(jobKey) kısmı, işin benzersiz kimliğini belirler.
            .AddTrigger(trigger => trigger //metodu, bir işe tetikleyici eklemek için kullanılır.
            .ForJob(jobKey) //bu tetikleyicinin hangi iş için olduğunu belirtir. Yani, bu tetikleyici, önce tanımlanan LoggingBackgoundJob işine bağlanır.
            .WithSimpleSchedule(schedule => schedule //basit bir zamanlama planı belirlenir. Bu plan, belirli bir aralıkta çalışacak şekilde ayarlanır.
            .WithIntervalInSeconds(5) //tetikleyicinin her 5 saniyede bir çalışması sağlanır.
            .RepeatForever())); // tetikleyicinin sürekli olarak tekrarlanması sağlanır, yani belirtilen aralıklarla sonsuz bir şekilde devam eder.

        //var jobKey = JobKey.Create(nameof(LoggingBackgoundJob));
        //options.AddJob<LoggingBackgoundJob>(jobBuilder => jobBuilder
        //    .WithIdentity(jobKey))
        //    .AddTrigger(trigger => trigger
        //        .ForJob(jobKey)
        //        .WithCronSchedule("0 * * ? * *")); // Her dakika 0. saniyesinde çalışacak "0 * * ? * *" ifadesi aşağıdaki şekilde ayrıştırılır:Saniye(0): Her dakikanın 0.saniyesi.Dakika(*): Herhangi bir dakika.Saat(*): Herhangi bir saat.Gün(?): Günü belirtilen ay veya hafta gününe sabitleme işlevi görmüyor(genellikle "?" karakteri kullanılır). Ay(*): Herhangi bir ay. Hafta günü(*): Herhangi bir hafta günü.
    }
}