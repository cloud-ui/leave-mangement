<template>
<div>
    <comp-workerview v-if="type === 'look'" :id="workerId"></comp-workerview>
    <comp-addworker @close="closeForm" v-if="type==='add'"></comp-addworker>
</div>
</template>
<script>
import CompWorkerview from './workerview'
import CompAddworker from './addworker'
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
        CompAddworker
    },
    methods:{
        closeForm(val){
            this.$emit('close',val)
        }
    }
}
</script>
