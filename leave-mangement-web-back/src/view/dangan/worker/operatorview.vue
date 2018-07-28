<template>
<div>
    <comp-workerview v-if="type === 'look'" :id="workerId"></comp-workerview>
    <comp-addworker @close="closeForm" v-if="type==='add'"></comp-addworker>
    <comp-transfer :id="workerId" v-if="type==='transfer'"></comp-transfer>
</div>
</template>
<script>
import CompWorkerview from './workerview'
import CompAddworker from './addworker'
import CompTransfer from './transfer'
export default {
    props:['workerId','type'],
    watch:{
        workerId:{
        handler(val, olaval) {
          this.workerId = val
        },
        deep: true
        },
    },
    components:{
        CompWorkerview,
        CompAddworker,
        CompTransfer
    },
    methods:{
        closeForm(val){
            this.$emit('close',val)
        }
    }
}
</script>
