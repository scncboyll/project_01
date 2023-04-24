window.onload = function () {
    Meaaage();
}

//读取数据库中的数据，进行页面的渲染
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
            str+=`  
            <li class="list-group-item">
                <span>${userList[i].msg}</span>`
            if(userList[i].hfmsg === ''){
                str+=`<span>管理员暂未回复</span>`
            }else{
                str+=`<span>${userList[i].hfmsg}</span>`
            }
            str+=`
            <span class="badge" style="background-color: #5BC0DE;">评论人：${userList[i].chatname}</span>
                <span class="badge" style="background-color: #F0AD4E;">评论时间：${userList[i].savetime}</span>
            </li>`

        }
        document.getElementById("userList").innerHTML = str;


    })

}


//发表留言
$('.btn').on('click',function(){
    addMessage()
})
//添加留言
function addMessage() {
    const messagename = document.querySelector('input[name="username"]').value;
    const contents = document.querySelector('textarea[name="content"]').value;
    

    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "goodsMessage",
        Messagename: messagename,
        Contents:contents,
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {

        if (data.Data > 0) {
            alert("发表成功！");
             Meaaage()
        } else {
            alert("发表失败！");
        }


    })

}

