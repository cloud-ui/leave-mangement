<template>
    <el-tabs v-model="activeName" @tab-click="handleClick">
        <el-tab-pane label="添加员工" name="first">
            <div>
                <el-form :inline="true" :model="data" label-width="82px" class="demo-form-inline">
                    <el-form-item label="姓名：">
                        <el-input v-model="data.name"></el-input>
                    </el-form-item>
                    <el-form-item label="性别：">
                        <el-radio-group v-model="data.sex">
                            <el-radio :label="0">女</el-radio>
                            <el-radio :label="1">男</el-radio>
                        </el-radio-group>
                    </el-form-item>
                    <el-form-item label="证件类型：">
                        <el-select v-model="data.paperType">
                            <el-option v-for="item in paperTypes" :key="item.id" :label="item.value" :value="item.id"></el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="证件号码：">
                        <el-input v-model="data.paperNumber"></el-input>
                    </el-form-item>
                    <el-form-item label="公司部门：">
                        <comp-dep-select :depId="data.departmentId" @change="changeDep" v-model="data.departmentId"></comp-dep-select>
                    </el-form-item>
                    <el-form-item label="公司职位：">
                        <el-select v-model="data.positionId">
                            <el-option v-for="item in positionData" :key="item.id" :label="item.name" :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="入职状态：">
                        <el-select v-model="data.state">
                            <el-option v-for="item in stateData" :key="item.id" :label="item.name" :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="入职时间：">
                        <el-date-picker type="date" v-model="data.entryTime"></el-date-picker>
                    </el-form-item>
                    <el-form-item style="display: flex;justify-content: flex-end;padding-right: 10px;">
                        <el-button type="primary" @click="handleSingleSubmit()">提交</el-button>
                    </el-form-item>
                </el-form>
            </div>
        </el-tab-pane>
        <el-tab-pane label="批量添加员工" name="second">
            <!-- <comp-mulitdep></comp-mulitdep> -->
        </el-tab-pane>
    </el-tabs>
</template>
<script>
    import CompDepSelect from '@/packages/components/dep-select'
    import {
        FileApi
    } from '../api.js'
    export default {
        components: {
            CompDepSelect
        },
        data() {
            return {
                activeName: 'first',
                data: {
                    name: '',
                    positionId: '',
                    departmentId: '',
                    sex: '',
                    paperType: '',
                    paperNumber: '',
                    entryTime: '',
                    state: '',
                },
                paperTypes: [{
                    id: 1,
                    value: '居民身份证'
                }, {
                    id: 2,
                    value: '海外护照'
                }],
                positionData: [],
                stateData: [],
            }
        },
        mounted() {
            this.loadPosition()
            this.loadState()
        },
        methods: {
            //加载职位选择器
            loadPosition() {
                FileApi.getPositionList().then(res => {
                    this.positionData = res.data
                })
            },
            loadState() {
                FileApi.getStateList().then(res => {
                    this.stateData = res.data
                })
            },
            changeDep(val){
                this.data.departmentId = val === ""?0:val
            },
            //单独添加员工
            handleSingleSubmit(){
                this.data.entryTime = this.data.entryTime.getTime()
                FileApi.addSingleWorker(this.data).then(res=>{
                    const type1 = res.data.isSuccess?'success': 'error'
                    this.$message({ type: type1,message: res.data.message});
                    this.closeForm()
                })
            },
            handleClick(tab, event) {
                console.log(tab, event);
            },
            closeForm(){
                this.$emit('close',false)
            }
        }
    }
</script>

