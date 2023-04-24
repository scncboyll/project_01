
window.onload = function () {
    QueryTask();
}

// 获取物流信息
function QueryTask() {
    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "Adminlogistics",
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<tr>`;
            str += `<td>${userList[i].logid}</td>`;
            str += `<td>${userList[i].Shiptime}</td>`;
            if (userList[i].recetime == '') {
                str += `<td>${userList[i].wlinfo}</td>`;
            } else {
                str += `<td>${userList[i].recetime}</td>`;
            }
            str += `<td>${userList[i].shraddr}</td>`;
            str += `<td>${userList[i].shrtel}</td>`;
            str += `<td>${userList[i].logname}</td>`;
            str += `<td>${userList[i].tel}</td>`;
            str += `<td>${userList[i].wlinfo}</td>`;
            str += `<td>${userList[i].email}</td>`;;
            str += `<td><button type="button" onclick="to_dellogistics(${userList[i].logid})")">删除</button></td>`;
            str += `<tr>`;

        }
        document.getElementById("userList").innerHTML = str;

    })

}


//删除物流信息
function to_dellogistics(id) {
    const confirm = window.confirm("确定删除这条物流信息吗？");
    console.log(id);
    if (confirm) {
        let url = "/API";

        let param = {
            APICommand: "Admindellogistics",
            ID: id
        }
        getAPICommandData(url, param).then(data => {

            //window.location.reload();  重新加载

            if (data.Data > 0) {
                alert("删除成功");
                QueryTask();
            } else {
                alert(data);
            }
        })
    }
}