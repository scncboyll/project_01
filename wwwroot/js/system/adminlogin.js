function demoRequest() {
    // 请求URL
    var url = "/API";
    // 请求JSON格式参数
    var parameter = {
        APICommand: "demo",
        demo: 42
    };
    getAPICommandData(url, parameter).then(data => {
        // 返回JSON数据
        console.log(data);
    });
}

demoRequest();

//登录函数
function Login() {
    let url = "/API";
    let adminName = document.getElementById("name").value;
    let adminPwd = document.getElementById("password").value;




    let val1 = /^[\u4e00-\u9fa5]{1}[\u4e00-\u9fa50-9]{1,5}$/;//只允许中文 1-5个中文
    let val2 = /^[a-zA-Z]{1}[a-zA-Z0-9]{4,15}$/;//长度为5-10位的字母数字的组合，必须字母开头

    if (!val1.test(adminName)) {
        alert("用户名必须为2-6个中文或数字的组合,必须是中文开头");
        return;
    }
    if (!val2.test(adminPwd)) {
        alert("密码必须是长度为5-10位的字母数字的组合,必须字母开头");
        return;
    }
    // userPwd = md5(userPwd);
    let parameter = {
        APICommand: "AdminLogin",
    }

    getAPICommandData(url, parameter).then(data => {
        console.log(data.Data);
        let res = data.Data;
        let flag = true;
        for (let i = 0; i < res.length; i++) {
            if (res[i].AdminName == adminName && res[i].AdminPwd == adminPwd) {
                flag = true;
                window.location.href = "Home?DashboardID=4";//给url传了一个DashboardID = 7，在common/Home中判断并进行跳转
                break;
            } else {
                flag = false;
            }

        }
        if(flag==false){
            alert('请检查用户名密码是否正确')
        }
        console.log(flag);

    })
}


//点击登录 调用登录函数
let but = document.getElementById("loginBut");
but.addEventListener("click", Login);