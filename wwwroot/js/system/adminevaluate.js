
window.onload = function () {
    QueryTask();
}

function QueryTask() {
    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "Admineval",
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<tr>`;
            str += `<td>${userList[i].goodid}</td>`;
            str += `<td>${userList[i].jb}</td>`;
            str += `<td>${userList[i].msg}</td>`;
            str += `<td>${userList[i].hf}</td>`;
            str += `<td>${userList[i].evaluname}</td>`;
            str += `<td>${userList[i].savetime}</td>`;
            str += `<td><button type="button" onclick="evaldel(${userList[i].id})">删除</button></td>`;
            str += `<tr>`;

        }
        document.getElementById("userList").innerHTML = str;
    })

}


//删除评价信息
function evaldel(id) {
    const confirm = window.confirm("确定删除这个评价吗？");
    console.log(id);
    if (confirm) {
        let url = "/API";

        let param = {
            APICommand: "Admindeleval",
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
