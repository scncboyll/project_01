
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
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<tr>`;
            str += `<td>${userList[i].UserName}</td>`;
            str += `<td>${userList[i].UserPwd}</td>`;
            str += `<td>${userList[i].Tname}</td>`;
            str += `<td>${userList[i].SaveTime}</td>`;
            str += `<td><button type="button" onclick="to_changedata(${userList[i].UserID})")">修改</button></td>`;
            str += `<td><button type="button" onclick="del(${userList[i].UserID})">删除</button></td>`;
            str += `<tr>`;

        }
        document.getElementById("userList").innerHTML = str;



        // if (data.Data > 0) {
        //     window.location.href = "/Home?DashboardID=4";//给url传了一个DashboardID = 1，在common/Home中判断并进行跳转
        // }
    })

}


//删除用户信息
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


//修改用户
//编辑跳转页面
function to_changedata(res) {
    let right = document.getElementsByClassName('right')[0];
    let wapper = document.getElementsByClassName('wapper')[0];
    right.style.display = 'none';
    wapper.style.display = 'block'

    let id = res;

    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "find",
        ID: id
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<h1>修改用户信息</h1>`;
            str += `<div class="top">`;
            str += `<ul>`;
            str += `<li><i>用户名：</i> <input type="text" placeholder="请输入用户名" value = "${userList[i].UserName}" id="findname"></li>`;
            str += `<li><i>密码：</i> <input type="text" placeholder="请输入密码" value = "${userList[i].UserPwd}" id="findpwd"></li>`;
            str += `<li><i>电子邮箱：</i> <input type="text" placeholder="请输入电子邮箱" value = "${userList[i].Email}" id="findemail"></li>`;
            str += `<li><i>真实姓名：</i> <input type="text" placeholder="请输入真实姓名" value = "${userList[i].Tname}" id="findtname"></li>`;
            str += `<li><i>性别：</i> <input type="text" placeholder="请输入性别" value = "${userList[i].Sex}" id="findsex"></li>`;
            str += `<li><i>地址：</i> <input type="text" placeholder="请输入居住地址" value = "${userList[i].Addr}" id="findaddr"></li>`;
            str += `<li><i>联系电话：</i> <input type="text" placeholder="请输入联系电话" value = "${userList[i].Tel}" id="findtel"></li>`;
            str += `</ul>`;
            str += `</div>`;
            str += `<div class="submit-w3l">`;
            str += `<button id="loginBut" class="button" onclick="userChange(${userList[i].UserID})">提交修改信息</button>`;
            str += `</div>`;

        }
        document.getElementById("wapper").innerHTML = str;
        
    })

}


// 点击按钮，修改用户的信息
function userChange(res) {
    let FindName = document.getElementById("findname").value;
    let Findpwd = document.getElementById("findpwd").value;
    let Findemail = document.getElementById("findemail").value;
    let Findtname = document.getElementById("findtname").value;
    let Findsex = document.getElementById("findsex").value;
    let Findaddr = document.getElementById("findaddr").value;
    let Findtel = document.getElementById("findtel").value;
    let FindUserID = res;
    console.log(res);

    let url = "/API";

    let parameter = {
        APICommand: "userChange",
        findname: FindName,
        findpwd: Findpwd,
        findemail: Findemail,
        findtname: Findtname,
        findsex: Findsex,
        findaddr: Findaddr,
        findtel: Findtel,
        finduserid: FindUserID
    }

    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        if (data.Data > 0) {
            alert("修改成功！");
            location.href="Home?DashboardID=8"
        } else {
            alert("修改失败！");
        }
    })

}