<template>
    <el-tabs v-model="activeName" @tab-click="handleClick">
    <el-tab-pane label="添加部门" name="first">
        <comp-singledep :formInfo="formInfo" @close="closeForm"></comp-singledep>
    </el-tab-pane>
    <el-tab-pane label="批量添加部门" name="second">
        <comp-mulitdep></comp-mulitdep>
    </el-tab-pane>
  </el-tabs>
</template>
<script>
import CompSingledep from './singledep'
import CompMulitdep from './mulitdep'
export default {
    data(){
        return{
            activeName:'first',
            formInfo:{
                type:'add',
                formData:{
                name:'',
                mangerId:'0',
                workerCount:0,
            },
            }            
        }
    },
    components:{
        CompSingledep,
        CompMulitdep
    },
    methods: {
      handleClick(tab, event) {
        console.log(tab, event);
      },
      closeForm(val){
          this.$emit('close',val)
      }
    }
}
</script>


