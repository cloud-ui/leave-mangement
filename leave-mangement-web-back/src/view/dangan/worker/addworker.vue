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
            <div>
                <div class="add-tip">
                    <ul>
                        <li>1.批量上传文件，需下载文件模板</li>
                        <li>2.下载模板填写内容</li>
                        <li>3.上传文件</li>
                    </ul>
                </div>
                <div class="row-item">
                    <div class="title">数据模板下载：</div>
                    <div class="link">
                        <a :class="linkA" href="http://localhost:57503/api/User/DownloadFile">点击下载模板</a>
                    </div>
                </div>
                <div class="row-item">
                    <div class="title">选择数据文件：</div>
                    <div class="upload">
                        <el-upload class="upload-demo" ref="upload" action="https://jsonplaceholder.typicode.com/posts/" accept=".xlsx" :on-change="handleChangeFile" :file-list="fileList" :show-file-list="false" :auto-upload="false">
                            <el-button class="select-file-button" slot="trigger" plain>{{selectFile}}</el-button>
                            <i class="el-icon-circle-close clear-file-button" v-show="clearFileIsShow" @click="clearFile"></i>
                            <el-button class="import-data-button" plain @click="importData">导入数据</el-button>
                        </el-upload>
                    </div>
                </div>
                <div v-if="showResult" class="show-result">
                    <p class="show-result-tip">总共导入 <span>{{successCount + badCount}}</span> 条数据， 成功 <span class="show-result-tip-s">{{successCount}}</span> 条，失败 <span class="show-result-tip-b">{{badCount}}</span> 条
                    </p>
                    <div>
                        <el-table :data="result" height="250" style="width: 100%">
                            <el-table-column prop="name" label="姓名" width="80">
                            </el-table-column>
                            <el-table-column prop="account" label="账号" width="170">
                            </el-table-column>
                            <el-table-column prop="company" align="center" label="公司名称">
                            </el-table-column>
                            <el-table-column prop="department" align="center" label="部门名称">
                            </el-table-column>
                            <el-table-column prop="position" align="center" label="职位">
                            </el-table-column>
                            <el-table-column prop="state" align="center" label="职位状态">
                            </el-table-column>
                            <el-table-column prop="entryTime" align="center" label="入职时间">
                            </el-table-column>
                            <el-table-column prop="sex" align="center" label="性别">
                            </el-table-column>
                            <el-table-column prop="phoneNumber" align="center" label="联系方式" width="170">
                            </el-table-column>
                            <el-table-column prop="address" align="center" label="地址">
                            </el-table-column>
                            <el-table-column prop="paperType" align="center" label="证件类型" width="110">
                            </el-table-column>
                            <el-table-column prop="paperNumber" align="center" label="证件号码" width="200">
                            </el-table-column>
                            <el-table-column prop="isSuccess" align="center" label="是否成功">
                                <template slot-scope="scope">
                                    <i :class="scope.row.isSuccess===true?`el-icon-circle-check show-result-tip-s`:`el-icon-circle-close show-result-tip-b`"></i>
                                </template>
                            </el-table-column>
                        </el-table>
                    </div>
            </div>
        </div>
        </el-tab-pane>
    </el-tabs>
</template>
<script>
    import CompDepSelect from "@/packages/components/dep-select";
    import {
        FileApi
    } from "../api.js";
    import '../dangan.scss'
    export default {
        components: {
            CompDepSelect
        },
        data() {
            return {
                activeName: "first",
                data: {
                    name: "",
                    positionId: "",
                    departmentId: "",
                    sex: "",
                    paperType: "",
                    paperNumber: "",
                    entryTime: "",
                    state: ""
                },
                paperTypes: [{
                        id: 1,
                        value: "居民身份证"
                    },
                    {
                        id: 2,
                        value: "海外护照"
                    }
                ],
                positionData: [],
                stateData: [],
                //批量添加需要的内容
                fileList: [],
                selectFile: '选择文件',
                clearFileIsShow: false,
                linkA: 'link-click',
                showResult: false,
                successCount: 0,
                badCount: 0,
                result: '',
            };
        },
        mounted() {
            this.loadPosition();
            this.loadState();
        },
        methods: {
            //加载职位选择器
            loadPosition() {
                FileApi.getPositionList().then(res => {
                    this.positionData = res.data;
                });
            },
            loadState() {
                FileApi.getStateList().then(res => {
                    this.stateData = res.data;
                });
            },
            changeDep(val) {
                this.data.departmentId = val === "" ? 0 : val;
            },
            //单独添加员工
            handleSingleSubmit() {
                this.data.entryTime = this.data.entryTime.getTime();
                FileApi.addSingleWorker(this.data).then(res => {
                    const type1 = res.data.isSuccess ? "success" : "error";
                    this.$message({
                        type: type1,
                        message: res.data.message
                    });
                    this.closeForm();
                });
            },
            handleClick(tab, event) {
                console.log(tab, event);
            },
            closeForm() {
                this.$emit("close", false);
            },
            //批量添加
            //删除文件
            clearFile(file, fileList) {
                this.$refs.upload.clearFiles();
                this.selectFile = "选择文件"
                this.clearFileIsShow = false
            },
            //文件改变
            handleChangeFile(file, fileList) {
                this.fileList = fileList.slice(-1);
                this.selectFile = file.name
                this.clearFileIsShow = true
            },
            importData() {
                //判断文件类型
                var ext, idx
                idx = this.selectFile.lastIndexOf(".");
                if (idx != -1) {
                    ext = this.selectFile.substr(idx + 1).toUpperCase();
                    ext = ext.toLowerCase();
                    if (ext != "xlsx") {
                        this.$message.error('请选择.xlsx文件！');
                        return;
                    }
                } else {
                    this.$message.error('请选择.xlsx文件！');
                    return;
                }
                //上传文件部分
                var files = [];
                this.fileList.forEach(file => {
                    files.push(file.raw);
                });
                console.log(files)
                FileApi.addMulitWorker({}, files).then(res => {
                    console.log(res)
                    this.showResult = true
                    this.successCount = res.data.data.successCount
                    this.badCount = res.data.data.badCount
                    this.result = res.data.data.data
                })
            }
        }
    };
</script>

