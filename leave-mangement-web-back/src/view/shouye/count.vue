<template>
    <div>
        <div class="kq-count-box">
            <comp-count class="kq-count-box-item" :count="workerCount" name="名员工" linkName="/worker/-1" background="#00c0ef" iconName="icon-yuangongzhanghaoguanli"></comp-count>         
            <comp-count class="kq-count-box-item"  :count="deparmentCount" name="个部门" linkName="/deparment" background="#00a65a" iconName="icon-zuzhibumen"></comp-count>
        </div>
    </div>
</template>
<script>
import './shouye.scss'
import CompCount from '../../packages/components/count'
export default {
    props:['workerCount','deparmentCount'],
    components:{
        CompCount
    },
    data(){
        return{
            count:150,
        }
    }
}
</script>
<style lang="scss" scoped>
.kq-count-box{
    display: flex;
    justify-content: flex-start;
    padding-bottom: 20px;
    &-item{
        margin-right: 30px;
    }
}

</style>


