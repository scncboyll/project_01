// //注册的方法
// function Register() {
//     let url = "/API";
//     let userName = document.getElementById("UserName").value;
//     let userPwd = document.getElementById("UserPwd").value;
//     let email = document.getElementById("Email").value;
//     let tname = document.getElementById("Tname").value;
//     let sex = document.getElementById("Sex").value;
//     let addr = document.getElementById("Addr").value;
//     let tel = document.getElementById("Tel").value;



//     let val1 = /^[\u4e00-\u9fa5]{1}[\u4e00-\u9fa50-9]{1,5}$/;//只允许中文 1-5个中文
//     let val2 = /^[a-zA-Z]{1}[a-zA-Z0-9]{4,15}$/;//长度为5-10位的字母数字的组合，必须字母开头
//     if (!val1.test(userName)) {
//         alert("用户名必须为2-6个中文或数字的组合,必须是中文开头");
//         return;
//     }
//     if (!val2.test(userPwd)) {
//         alert("密码必须是长度为5-10位的字母数字的组合,必须字母开头");
//         return;
//     }

//     // userPwd = md5(userPwd);
//     let parameter = {
//         APICommand: "Register",
//         UserName: userName,
//         UserPwd: userPwd,
//         Email: email,
//         Tname: tname,
//         Sex: sex,
//         Addr: addr,
//         Tel: tel
//     }

//     getAPICommandData(url, parameter).then(data => {
//         console.log(data);
//         if (data.Data > 0) {
//             alert("注册成功！");
//             setInterval(() => {
//                 window.location.href = "/Home?DashboardID=0";
//             }, 500)
//         } else {
//             alert("注册失败！");
//         }
//     })
// }


// //点击注册 调用注册函数
// let register = document.getElementById("Register");

// register.addEventListener("click", Register);
