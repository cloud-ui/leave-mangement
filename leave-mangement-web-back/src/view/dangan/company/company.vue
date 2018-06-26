<template>
<el-card style="width:82%" class="box-card">
    <el-form v-model="companyInfo" label-width="100px">
        <el-form-item label="公司名称:">
            <el-input v-model="companyInfo.name" :readonly="true" style="width:40%;"></el-input>
        </el-form-item>
        <el-form-item label="成立时间:">
            <el-input v-model="companyInfo.createTime" :readonly="true" style="width:40%;"></el-input>
        </el-form-item>
        <el-form-item label="公司地址：">
            <el-input></el-input>
        </el-form-item>
        <el-form-item label="公司法人:">
            <el-input v-model="companyInfo.corporation" :readonly="true" style="width:40%;"></el-input>
        </el-form-item>
        <el-form-item label="固定电话:">
            <el-input v-model="companyInfo.cellphoneNumber" :readonly="true" style="width:40%;"></el-input>
        </el-form-item>
        <el-form-item label="部门数量:">
            <el-input v-model="companyInfo.deparmentCount" :readonly="true" style="width:40%;"></el-input>
        </el-form-item>
        <el-form-item label="员工数量:">
            <el-input v-model="companyInfo.wokerCount" :readonly="true" style="width:40%;"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" size="mini">编辑</el-button>
            <el-button type="primary" size="mini">保存</el-button>
        </el-form-item>
    </el-form>
</el-card>
</template>
<script>
import {
        FileApi
    } from '../api.js'
export default {
    data(){
        return{
           companyInfo:{},
        }
    },
    mounted() {
            this.getCompanyInfo()
        },
        methods: {
            getCompanyInfo() {
                FileApi.getCompany().then(res => {
                    this.companyInfo = {
                        ...res.data
                    }
                    console.log(this.companyInfo)
                })
            },
            handleOpen(key, keyPath) {
                console.log(key, keyPath);
            },
            handleClose(key, keyPath) {
                console.log(key, keyPath);
            }
        }
}
</script>

