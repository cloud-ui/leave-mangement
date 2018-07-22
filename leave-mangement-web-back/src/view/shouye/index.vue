<template>
<div>
    <comp-count></comp-count>
</div>
</template>
<script>
import CompCount from './count'
export default{
    components:{
        CompCount,
    },
    data(){
        return{
            data:''
        }
    }
}
</script>