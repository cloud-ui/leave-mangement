<template>
    <div>
        <div class="kq-count-box">
            <comp-count class="kq-count-box-item" :count="count" name="名员工" linkName="/home" background="#00c0ef" iconName="icon-yuangongzhanghaoguanli"></comp-count>         
            <comp-count class="kq-count-box-item"  :count="count" name="个部门" linkName="/home" background="#00a65a" iconName="icon-zuzhibumen"></comp-count>
        </div>
    </div>
</template>
<script>
import './shouye.scss'
import CompCount from '../../packages/components/count'
export default {
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


