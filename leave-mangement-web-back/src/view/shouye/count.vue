<template>
    <div>
        <div >
            <comp-count :count="count" name="名员工" linkName="/home" background="#00c0ef" iconName="icon-yuangongzhanghaoguanli"></comp-count>         
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

