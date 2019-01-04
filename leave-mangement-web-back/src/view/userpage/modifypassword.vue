<template>
    <el-form :inline="true" class="demo-form-inline" ref="password" label-width="120px" :rules="rule" :model="formData">
        <el-form-item prop="password" label="原始密码：">
            <el-input type="password" v-model="formData.password"></el-input>
        </el-form-item>
        <el-form-item prop="newPassword" label="新密码：">
            <el-input type="password" v-model="formData.newPassword"></el-input>
        </el-form-item>
        <el-form-item prop="reNewPassword" label="重复新密码：">
            <el-input type="password" v-model="formData.reNewPassword"></el-input>
        </el-form-item>
        <el-form-item style="display:flex;justify-content: flex-end;">
            <el-button @click="handleClose">取消</el-button>
            <el-button @click="handleSubmit()" :loading="loading" type="primary">提交修改</el-button>
        </el-form-item>
    </el-form>
</template>
<script>
import {UserPageApi} from './api.js'
export default {
    props:['workerId'],
    data(){
        var checkPassword=(rule, value, callback)=>{
            if (!value) {
                return callback(new Error('新密码不能为空'));
            }else if(value === this.formData.password){
                return callback(new Error('新密码与旧密码一致'))
            }
            callback();
        };
        var checkNewPassword=(rule,value,callback)=>{
            if(!value){
                return callback(new Error('重复密码不能为空'));
            }else if(value !== this.formData.newPassword){
                return callback(new Error('重复密码与新密码不一致'));
            }
            callback();
        };
        return{
            formData:{
                id:this.workerId,
                password:'',
                newPassword:'',
                reNewPassword:'',
            },
            loading:false,
            rule:{
                password:[{ required: true, message: '请输入原始密码', trigger: 'blur'}],
                newPassword:[{ required: true, validator: checkPassword, trigger: 'blur'}],
                reNewPassword:[{required: true, validator: checkNewPassword, trigger: 'blur'}]
            }
        }
    },
    watch:{
        workerId:{
            handler(val,oldVal){
                this.formData.id = val
            },
            deep: true
        }
    },
    methods:{
        handleSubmit(){
            this.$refs['password'].validate((valid) => {
                if (valid) {
                    this.loading = true
                    UserPageApi.modifyPassword(this.formData).then(res=>{
                        const type1 = res.data.isSuccess?'success':'error'
                        this.$message({ type: type1,message: res.data.message});
                        this.handleClose()
                        this.$store.dispatch('accountLogoutSubmit').then(res => {
                            this.$router.push('/login');
                        }).catch(err => {
                        })
                    })
                }})
        },
        handleClose(){
            this.loading = false
            this.formData = {
                id:this.workerId,
                password:'',
                newPassword:'',
                reNewPassword:'',}
            this.$emit('closeForm',false)
            }
            
    }
}
</script>
