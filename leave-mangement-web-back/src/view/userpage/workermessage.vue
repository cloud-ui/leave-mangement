<template>
    <el-form ref="message" :inline="true" :model="data" label-width="100px" :rules="rule">
        <el-form-item prop="phoneNumber" label="联系方式：">
            <el-input style="width:250px;" v-model="data.phoneNumber"></el-input>
        </el-form-item>
        <el-form-item prop="address" label="家庭住址：">
            <el-input style="width:250px;" v-model="data.address"></el-input>
        </el-form-item>
        <el-form-item style="display:flex;justify-content: flex-end;">
            <el-button @click="handleClose">取消</el-button>
            <el-button @click="handleSubmit()" :loading="loading" type="primary">提交修改</el-button>
        </el-form-item>
    </el-form>
</template>

<script>
    import {
        UserPageApi
    } from './api.js'
    export default {
        props: ['workerId'],
        data() {
            return {
                data: {
                    id: this.workerId,
                    phoneNumber: '',
                    address: '',
                },
                loading: false,
                rule: {
                    phoneNumber: [{
                        required: true,
                        message: '请输入联系方式',
                        trigger: 'blur'
                    }],
                    address: [{
                        required: true,
                        message: '请输入地址',
                        trigger: 'blur'
                    }]
                }
            }
        },
        methods: {
            handleSubmit() {
                this.$refs['message'].validate((valid) => {
                    if (valid) {
                        this.loading = true
                        UserPageApi.editUserMessage(this.data).then(res => {
                            const type1 = res.data.isSuccess ? 'success' : 'error'
                            this.$message({
                                type: type1,
                                message: res.data.message
                            });
                            this.handleClose()
                        })
                    }
                })
            },
            handleClose() {
                this.loading = false
                this.data = {
                    phoneNumber: '',
                    address: '',
                }
                this.$emit('closeForm', false)
            }
        }
    }
</script>

