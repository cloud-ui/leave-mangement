<template>
<div>
  <div class="add-head">
      <div class="head-container">
          <div style="padding-top:15px;">
            <p class="head-title">人事考勤系统</p>
            <span class="add-company">添加公司</span>
          </div>
      </div>
  </div>  
  <div class="add-body">
      <div class="body-container">
          <div class="grid-bar">
              进度
          </div>
          <div class="add-box">
              1111
          </div>
      </div>
  </div>
</div>
</template>
<script>
export default{

}
</script>
<style lang="scss" scoped>
.add-head{
    width:100%;
    height: 76px;
    border-bottom: 1px solid #e3e3e3;
    box-shadow: 0 0 2px #E3E3E3;
}
.add-body{
    width:100%;
    height: 581px;
    background-color: #F9F9F9;
}
.head-container,.body-container{
    width: 990px;
    height: 100%;
    margin: auto;
}
.head-title{
    line-height: 40px;
    font-weight: 400;
    font-size: 25px;
    border-right: 2px solid gray;
    width: 160px;
    height: 40px;
    float: left;
}
.add-company{
    padding: 0px 0 0 10px;
    font-size: 20px;
    color: gray;
    line-height: 58px;
    font-weight: 100;
}
.grid-bar{
    width: 80%;
    margin: auto;
    padding: 30px 0px 30px 0px;
}
.add-box{
    width: 80%;
    margin: auto;
    height: 480px;
    box-shadow: 0 0 5px #c7c5c5;
    background: #fff;
    border: 1px solid #e7e7e7;
    padding: 30px 50px;
}
</style>
