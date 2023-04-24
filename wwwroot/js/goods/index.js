
window.onload = function () {
    QueryTask();
}

function QueryTask() {
    let url = "/API";

    //向APICommand传递一个AdminIndex参数
    let parameter = {
        APICommand: "Admingoods",
    }

    //向页面渲染数据
    getAPICommandData(url, parameter).then(data => {
        console.log(data);

        localStorage.setItem('datalist',JSON.stringify(data))

        let userList = data.Data;
        let userList_xcls=[]
        let userList_shyp=[]
        let userList_wjyp=[]
        let userList_xxsg=[]
        let userList_dzcp=[]
        // 通过分类获取数组
        for(let k in userList){
        if(userList[k].type === '1'){
            userList_xcls.push(userList[k])
        }else if(userList[k].type === '2'){
            userList_shyp.push(userList[k])
        }else if(userList[k].type === '3'){
            userList_wjyp.push(userList[k])
        }else if(userList[k].type === '4'){
            userList_xxsg.push(userList[k])
        }else if(userList[k].type === '5'){
             userList_dzcp.push(userList[k])
        }
        }
        userList_add(userList,'userList_index')
        userList_add(userList_xcls,'userList_xcls')
        userList_add(userList_shyp,'userList_shyp')
        userList_add(userList_wjyp,'userList_wjyp')
        userList_add(userList_xxsg,'userList_xxsg')
        userList_add(userList_dzcp,'userList_dzcp')
       
       $('#nav_list li').on('click',function () { 
         // 1.点击的同时，得到当前li 的索引号
        let index =  $(this).index();
        // 3.让下部里面相应索引号的item显示，其余的item隐藏
        if(index ===0){
            $('.goodslist ul').eq(index).show().siblings().hide();
            $('.banner').show()
        }else{
            $('.goodslist ul').eq(index).show().siblings().hide();
            $('.banner').hide()
        }
        
        })

    })
    
}
// 渲染数据
function userList_add(arr,id) {  
    let str = "";
    for (let i = 0; i < arr.length; i++) {
        str += `<li style="margin: 10px;">`;
        str += `<div class="goodsimg" style="border: 1px solid #ccc; padding: 10px;" onclick="location.href='Home?DashboardID=17&id=${arr[i].goodid}'">`;
        str += `<img src="../../uploads/${arr[i].goodid}.jpg" alt="">`;
        str += `</div>`;
        str += `<div class="goodsinfo">`;
        str += `<h3>${arr[i].goodname}</h3>`;
        str += `<p>介绍：${arr[i].note}</p>`;
        str += `<p>价格: <span>${arr[i].price}</span> 元</p>`;
        str += `</div>`;
        str += `<div class="goodsnum">`;
        str += `<p>已出售：${arr[i].filename}</p>`;
        str += `<p>剩余库存：${arr[i].tprice}</p>`;
        str += `</div>`;
        str += ` <div class="goodsstate">`;
        str += `<p>销售状态：${arr[i].salestatus}</p>`;
        str += ` </div>`;
        str += `<div class="joincar">`;
        str += `<button class="btn" id="joincar" onclick="joincar(${arr[i].goodid})" >加入购物车</button>`;
        str += ` </div>`;
        str += ` </li>`;
        // str += `<td><button type="button" onclick="to_changedata(${userList[i].UserID})")">修改</button></td>`;
        // str += `<td><button type="button" onclick="del(${userList[i].UserID})">删除</button></td>`;
        // str += `<tr>`;
    }
    document.getElementById(id).innerHTML = str;
}


// 点击添加购物车
function joincar(id) {
    console.log(id);

    let url = "/API";

    let param = {
        APICommand: "joincar",
        ID: id
    }
    getAPICommandData(url, param).then(data => {
        //window.location.reload();

        if (data.Data > 0) {
            alert("加入成功");
            QueryTask();
        } else {
            alert(data);
        }
    })

}

// 搜索框
const input = document.querySelector('.search input')
const ul = document.querySelector('.search_nav .result-list')

// 监听事件 获得焦点  
input.addEventListener('focus', function () {
    ul.style.display = 'block'
})
// 监听事件 失去焦点
input.addEventListener('blur', function () {
    ul.style.display = 'none'
})




