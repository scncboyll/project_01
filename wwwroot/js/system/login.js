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
    let userName = document.getElementById("username").value;
    let userPwd = document.getElementById("password").value;




    let val1 = /^[\u4e00-\u9fa5]{1}[\u4e00-\u9fa50-9]{1,5}$/;//只允许中文 1-5个中文
    let val2 = /^[a-zA-Z]{1}[a-zA-Z0-9]{4,15}$/;//长度为5-10位的字母数字的组合，必须字母开头

    if (!val1.test(userName) || userName == null) {
        alert("用户名错误，请填写正确的用户名");
        //用户名必须为2-6个中文或数字的组合,必须是中文开头
        return;
    }
    if (!val2.test(userPwd) || userPwd == null) {
        alert("密码错误，请填写正确的密码");
        //密码必须是长度为5-10位的字母数字的组合,必须字母开头
        return;
    }


    let parameter = {
        APICommand: "userLogin",

    }

    getAPICommandData(url, parameter).then(data => {
        // console.log(data.Data);
        let res = data.Data;
        console.log(res);
        let flag = true;
        for (let i = 0; i < res.length; i++) {
            if (res[i].UserName == userName && res[i].UserPwd == userPwd) {
                localStorage.setItem('ykyname', userName)
                flag = true;
                window.location.href = "Home?DashboardID=7";//给url传了一个DashboardID = 7，在common/Home中判断并进行跳转
                break
            } else {
                flag = false;
            }
        }
        
        if (flag == false) {
            alert('请检查用户名密码是否正确')
        }

        // else if (res.UserName == userName) {
        //     alert('密码错误，请重新输入');
        // } else {
        //     alert('用户名错误，请重新输入');
        // }
        // if (data.Data > 0) {
        //     window.location.href = "Home?DashboardID=7";//给url传了一个DashboardID = 7，在common/Home中判断并进行跳转
        // } else {
        //     alert('没有找到用户信息，请先去注册');
        // }
    })
}

//点击登录 调用登录函数
let but = document.getElementById("loginBut");
but.addEventListener("click", Login);