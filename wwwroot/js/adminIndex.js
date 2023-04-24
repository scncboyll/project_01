
window.onload = function () {
    QueryTask();
}

function QueryTask() {
    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "AdminIndex",
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<tr>`;
            str += `<td>${userList[i].UserID}</td>`;
            str += `<td>${userList[i].UserName}</td>`;
            str += `<td>${userList[i].UserPwd}</td>`;
            str += `<td>${userList[i].Tname}</td>`;
            str += `<td>${userList[i].Sex}</td>`;
            str += `<td>${userList[i].Tel}</td>`;
            str += `<td>${userList[i].Email}</td>`;
            str += `<td>${userList[i].Addr}</td>`;
            str += `<td>${userList[i].SaveTime}</td>`;
            // str += `<td>${userList[i].Delstatus}</td>`;
            // str += `<td><button type="button" onclick="del(${userList[i].UserID})">删除</button></td>`;
            str += `<tr>`;
        }
        document.getElementById("userList").innerHTML = str;



        if (data.Data > 0) {
            window.location.href = "/Home?DashboardID=4";//给url传了一个DashboardID = 1，在common/Home中判断并进行跳转
        }
    })

}


//删除
function del(id) {
    const confirm = window.confirm("确定删除这个Task吗？");
    console.log(id);
    if (confirm) {
        let url = "/API";

        let param = {
            APICommand: "Admindel",
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
