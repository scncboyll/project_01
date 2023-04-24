window.onload = function () {
    Meaaage();
}

function Meaaage() {
    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "AdminMeaaage",
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);
        let userList = data.Data;
        let str = "";
        for (let i = 0; i < userList.length; i++) {
            str += `<tr>`;
            str += `<td>${userList[i].msg}</td>`;
            str += `<td>${userList[i].hfmsg}</td>`;
            str += `<td>${userList[i].chatname}</td>`;
            str += `<td>${userList[i].savetime}</td>`;
            str += `<td><button type="button" onclick="reply(${userList[i].chatid})">前往回复</button></td>`;
            str += `<td><button type="button" onclick="del(${userList[i].chatid})">删除</button></td>`;
            str += `<tr>`;

        }
        document.getElementById("userList").innerHTML = str;


    })

}

//回复留言
function reply(id){
    const replydiv = document.querySelector('.replydiv')
    replydiv.style.display = 'block'
    localStorage.setItem('messageid',id)
} 
//给提交按钮添加点击事件
$('.reply').on('click',function(){
    joinMessage();
     const replyconcent = document.querySelector('.form-control').value;
     console.log(replyconcent);
})

//提交留言信息
function joinMessage(){
    const replyconcent = document.querySelector('.form-control').value;
    const messageid = localStorage.getItem('messageid')
    console.log(replyconcent,messageid);
    const replydiv = document.querySelector('.replydiv')
    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "joinmessage",
        Replyconcent:replyconcent,
        Messageid:messageid
    }
    getAPICommandData(url, parameter).then(data => {
        if (data.Data > 0) {
            alert("加入成功");
            const replyconcent = document.querySelector('.form-control')
            replyconcent.value = ' ';
            Meaaage();
            replydiv.style.display = 'none'
        } else {
            alert(data);
        }
    })

}




//删除留言
function del(id) {
    const confirm = window.confirm("确定删除这个留言吗？");
    console.log(id);
    if (confirm) {
        let url = "/API";

        let param = {
            APICommand: "Admindelmessage",
            ID: id
        }
        getAPICommandData(url, param).then(data => {
            //window.location.reload();

            if (data.Data > 0) {
                alert("删除成功");
                Meaaage();
            } else {
                alert(data);
            }
        })
    }
}
