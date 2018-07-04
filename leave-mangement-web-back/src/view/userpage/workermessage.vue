<template>
    <el-form :inline="true" :model="data" label-width="100px">
        <el-form-item prop="age" label="年龄：">
            <el-input style="width:202px;" v-model="data.age"></el-input>
        </el-form-item>
        <el-form-item prop="birth" label="出生日期：">
            <el-date-picker style="width:220px;" v-model="data.birth" type="date" placeholder="选择日期">
            </el-date-picker>
        </el-form-item>
        <el-form-item prop="phoneNumber" label="联系方式：">
            <el-input style="width:202px;" v-model="data.phoneNumber"></el-input>
        </el-form-item>
        <el-form-item prop="address" label="家庭住址：">
            <el-input style="width:220px;" v-model="data.address"></el-input>
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
        data() {
            return {
                data: {
                    id:this.workerId,
                    age: '',
                    birth: '',
                    phoneNumber: '',
                    address: '',
                },
                loading:false,
            }
        },
        methods:{
            handleSubmit(){
                this.loading = true
                this.data.birth = this.data.birth.getTime()
                UserPageApi.editUserMessage(this.data).then(res=>{
                    const type1 = res.data.isSuccess?'success':'error'
                    this.$message({ type: type1,message: res.data.message});
                    this.handleClose()
                })
            },
            handleClose(){
                this.loading = false
                this.data={
                    age: '',
                    birth: '',
                    phoneNumber: '',
                    address: '',
                }
                this.$emit('closeForm',false)
            }
        }
    }
</script>

