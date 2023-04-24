//注册的方法
function AddTask(){
    let url = "/API";
    let task = document.getElementById("Task").value;


    let parameter = {
        APICommand:"AddTask",
        Task:task

    }

    getAPICommandData(url,parameter).then(data =>{
        console.log(data);
        if(data.Data>0){
            alert("添加成功！");
            setInterval(()=>{
                window.location.href = "/Home?DashboardID=1";
            },500)
        }else{
            alert("添加失败！");
        }
    })
}


//点击注册 调用注册函数
let task = document.getElementById("addTask");

task.addEventListener("click",AddTask);
