<template>
    <el-form ref="ruleForm2" :rules="rule" label-width="80px" class="demo-ruleForm">
        <el-form-item label="部门名称" prop="name">
            <el-input style="width:50%" type="name"  auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item label="部门经理" prop="manger">
            <el-input style="width:50%" type="manger"  auto-complete="off"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="submitForm('ruleForm2')">提交</el-button>
            <el-button @click="resetForm('ruleForm2')">重置</el-button>
        </el-form-item>
    </el-form>
</template>
<script>
export default {
    data(){
        return{
            rule:{
                name:[{required: true, message: '请输入名称', trigger: 'blur'}]
            }
        }
    },
    methods:{
        submitForm(form){

        },
        resetForm(form){

        }
    }
}
</script>

