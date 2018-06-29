<template>
    <el-tabs v-model="activeName" @tab-click="handleClick">
    <el-tab-pane label="添加员工" name="first">
        <!-- <comp-singledep :formInfo="formInfo" @close="closeForm"></comp-singledep> -->
    </el-tab-pane>
    <el-tab-pane label="批量添加员工" name="second">
        <!-- <comp-mulitdep></comp-mulitdep> -->
    </el-tab-pane>
  </el-tabs>
</template>
<script>
export default {
    data(){
        return{
            activeName:'first',
            data:{}
        }
    },
    methods:{
        handleClick(tab, event) {
        console.log(tab, event);
      },
    }
}
</script>

