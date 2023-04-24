//注册的方法
function Register() {
    let url = "/API";
    let adminName = document.getElementById("AdminName").value;
    let adminPwd = document.getElementById("AdminPwd").value;
    let realName = document.getElementById("RealName").value;
    let sex = document.getElementById("Sex").value;
    let idCard = document.getElementById("IdCard").value;
    let tel = document.getElementById("Tel").value;
    let addr = document.getElementById("Addr").value;
    let email = document.getElementById("Email").value;



    //对用户名和密码进行正则验证
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



    let parameter = {
        APICommand: "AdminRegister",
        AdminName: adminName,
        AdminPwd: adminPwd,
        RealName: realName,
        Sex: sex,
        IdCard: idCard,
        Tel: tel,
        Addr: addr,
        Email: email,
    }

    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        if (data.Data > 0) {
            alert("注册成功！");
            setInterval(() => {
                window.location.href = "/Home?DashboardID=5";
            }, 500)
        } else {
            alert("注册失败！");
        }
    })
}


//点击注册 调用注册函数
let register = document.getElementById("Register");

register.addEventListener("click", Register);
