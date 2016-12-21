using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class NurseEvent
    {
        public int ID { get; set; }
        [Display(Name = "姓名")]
        public string patient_name { get; set; }
        [Display(Name = "科室")]
        public string dept_name { get; set; }
        [Display(Name = "床号")]
        public string bed_no { get; set; }
        [Display(Name = "年龄")]
        public string patient_age { get; set; }
        [Display(Name = "性别")]
        [CheckValue("性别")]
        public string patient_sex { get; set; }
        [Display(Name = "费别")]
        public string charge_type { get; set; }
        [Display(Name = "疾病名称")]
        public string diagnosis { get; set; }
        [Display(Name = "住院号")]
        public string inp_no { get; set; }
        [Display(Name = "入院日期")]
        //[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime admission_date { get; set; }
        [Display(Name = "事件发生时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime event_happen_time { get; set; }
        [Display(Name = "事件经过说明")]
        [DataType(DataType.MultilineText)]
        public string event_info { get; set; }
        [Display(Name = "事件级别")]
        public string event_level { get; set; }
        [Display(Name = "提供何种服务时发生")]
        public string event_service { get; set; }
        [Display(Name = "所报不良事件名称")]
        public string event_name  { get; set; }
        [Display(Name = "所报不良事件主要情况")]
        [DataType(DataType.MultilineText)]
        public string event_fact { get; set; }
        [Display(Name = "与当事人可能相关的因素")]
        public string event_relevant_factor { get; set; }
        [Display(Name = "事件发生前病人所处状态")]
        public string event_patient_status { get; set; }
        [Display(Name = "给别人造成的功能损害")]
        public string event_harm { get; set; }
        [Display(Name = "给别人造成功能损害的轻重程度")]
        public string event_harm_grade { get; set; }
        [Display(Name = "事件发生所在地点")]
        public string event_location { get; set; }
        [Display(Name = "主要当事人一")]
        public string event_concerned_staff1 { get; set; }
        [Display(Name = "主要当事人二")]
        public string event_concerned_staff2 { get; set; }
        [Display(Name = "主要当事人三")]
        public string event_concerned_staff3 { get; set; }
        [Display(Name = "事件处理情况")]
        public string event_treatment { get; set; }
        [Display(Name = "改进措施")]
        public string event_treatment_after { get; set; }
        [Display(Name = "报告人")]
        public string reporter_name { get; set; }
        [Display(Name = "报告者代码")]
        public string reporter_id { get; set; }
        public string handled_by_headnurse { get; set; }
        public string handled_by_leader { get; set; }
    }
}