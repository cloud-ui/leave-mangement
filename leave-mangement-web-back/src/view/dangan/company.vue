<template>
    <div class="comp">
        <div class="comp-title">
            <p>公司资料</p>
        </div>
        <div class="comp-body">
            <el-form label-width="100px">
                <el-form-item label="公司名称">
                    <el-input v-model="companyInfo.name" :readonly="true" style="width:40%;"></el-input>
                </el-form-item>
                <el-form-item label="成立时间">
                    <el-input v-model="companyInfo.createTime" :readonly="true" style="width:40%;"></el-input>
                </el-form-item>
                <el-form-item label="公司法人">
                    <el-input v-model="companyInfo.corporation" :readonly="true" style="width:40%;"></el-input>
                </el-form-item>
                <el-form-item label="固定电话">
                    <el-input v-model="companyInfo.cellphoneNumber" :readonly="true" style="width:40%;"></el-input>
                </el-form-item>
                <el-form-item label="部门数量">
                    <el-input v-model="companyInfo.deparmentCount" :readonly="true" style="width:40%;"></el-input>
                </el-form-item>
                <el-form-item label="员工数量">
                    <el-input v-model="companyInfo.wokerCount" :readonly="true" style="width:40%;"></el-input>
                </el-form-item>
            </el-form>
        </div>
    </div>
</template>
<script>
import {FileApi} from './api.js'
import './dangan.scss';
export default {
    data(){
        return{
            companyInfo:{
            }
        }
    },
    mounted(){
        this.getCompanyInfo()
    },
    methods:{
        getCompanyInfo(){
            FileApi.getCompany().then(res=>{
                this.companyInfo={
                    ...res.data
                }
                console.log(this.companyInfo)
            })
        }
    }
}
</script>
<style lang="scss" scoped>

</style>


