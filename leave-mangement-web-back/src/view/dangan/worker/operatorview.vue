<template>
<div>
    <comp-workerview v-if="type === 'look'" :id="workerId"></comp-workerview>
    <comp-addworker v-if="type==='add'"></comp-addworker>
</div>
</template>
<script>
import CompWorkerview from './workerview'
import CompAddworker from './addworker'
export default {
    props:['workerId','type'],
    components:{
        CompWorkerview,
        CompAddworker
    }
}
</script>
