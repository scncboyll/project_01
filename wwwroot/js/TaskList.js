
window.onload = function () {
    QueryTask();
}

function QueryTask() {
    let url = "/API";

    let parameter = {
        APICommand: "QueryTask",
    }

    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let taskList = data.Data;
        let str = "";
        for (let i = 0; i < taskList.length; i++) {
            str += `<tr>`;
            str += `<td>${taskList[i].TaskID}</td>`;
            str += `<td>Porject</td>`;
            str += `<td>${taskList[i].Task}</td>`;
            str += `<td>ll</td>`;
            str += `<td>${taskList[i].TimeUpdated}</td>`;
            str += `<td>lei</td>`;
            str += `<td><button type="button" onclick="del(${taskList[i].TaskID})">删除</button></td>`;
            str += `<tr>`;
        }
        document.getElementById("taskList").innerHTML = str;



        if (data.Data > 0) {
            window.location.href = "/Home?DashboardID=1";//给url传了一个DashboardID = 1，在common/Home中判断并进行跳转
        }
    })

}



function del(id) {
    const confirm = window.confirm("确定删除这个Task吗？");
    if (confirm) {
        let url = "/API";

        let param = {
            APICommand: "del",
            ID: id
        }
        getAPICommandData(url, param).then(data => {
            //window.location.reload();
            
            if (data.Data > 0) {
                alert("删除成功");
                QueryTask();
            } else {
                alert(data);
            }
        })
    }
}
