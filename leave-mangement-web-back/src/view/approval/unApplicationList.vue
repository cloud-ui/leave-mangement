<template>
    <div class="index">
        <div class="index-title">
            <p>未提交申请</p>
        </div>
        <div class="index-body">
            <div class="index-body-title">
                <div style="display:flex;">
                    <p>共 <span>{{totalCount}}</span> 条未提交的申请</p>
                    <router-link to="/addApplication/-1" class="link"><i class="el-icon-plus"></i>添加申请</router-link>
                </div>
                <el-input style="width:30%" placeholder="请输入内容" v-model="query" class="input-with-select">
                    <el-button slot="append" @click="handleChangeQuery()">搜索</el-button>
                </el-input>
            </div>
            <el-table :data="tableData" :header-cell-style="headerCellStyle" border style="width: 100%">
                <el-table-column align="center" prop="id" label="序号" width="80">
                </el-table-column>
                <el-table-column align="center" prop="workerName" label="员工名称" width="100">
                </el-table-column>
                <el-table-column align="center" prop="type" label="请假类型" width="120">
                </el-table-column>
                <el-table-column align="center" prop="startTime" label="开始时间">
                    <template slot-scope="scope">
                        {{scope.row.startTime | formatDate}}
                    </template>
                </el-table-column>
                <el-table-column align="center" prop="endTime" label="结束时间">
                    <template slot-scope="scope">
                        {{scope.row.endTime | formatDate}}
                    </template>
                </el-table-column>
                <el-table-column align="center" prop="createTime" label="创建时间">
                </el-table-column>
                <el-table-column align="center" label="操作" width="80">
                    <template slot-scope="scope">
                        <el-dropdown>
                            <i class="el-icon-menu"></i>
                            <el-dropdown-menu slot="dropdown">
                                <el-dropdown-item>
                                 <el-button type="text" size="small" @click="handleLook(scope.row.id)" icon="el-icon-view">查看</el-button>
                                </el-dropdown-item>
                                <el-dropdown-item>
                                 <el-button @click="handleSubmit(scope.row.id)" style="color:#5fb878;" type="text" size="small" icon="el-icon-upload2">提交</el-button>
                                </el-dropdown-item>
                                <el-dropdown-item>
                                 <el-button @click="handleEdit(scope.row.id)" type="text" size="small" icon="el-icon-edit">编辑</el-button>
                                </el-dropdown-item>
                                <el-dropdown-item>
                                 <el-button @click="handleDelete(scope.row.id)" style="color:red;" type="text" size="small" icon="el-icon-delete">删除</el-button>
                                </el-dropdown-item>
                            </el-dropdown-menu>
                        </el-dropdown>
                    </template>
                </el-table-column>
            </el-table>
            <el-pagination 
            style="padding-top:10px"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page="currentPage"
            :page-sizes="[100, 200, 300, 400]"
            :page-size="pageSize"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalCount">
            </el-pagination>
        </div>
        <el-dialog title="申请详情" :visible.sync="dialogVisible" :before-close="handleClose">
         <comp-look @closeForm='handleClose' @close='closeForm' :id="applicationId" ref="compForm"></comp-look>
        </el-dialog>
    </div>
</template>
<script>
    import '../index.scss'
    import CompLook from './applicationView'
    import { ApprovalApi } from "./api.js";
    export default {
        components:{
            CompLook
        },
        data() {
            return {
                tableData: [],
                headerCellStyle: {
                    backgroundColor: '#f2f2f2',
                    fontSize: '14px',
                    color: '#434343',
                    fontWeight: 400
                },
                currentPage: 1,
                pageSize: 20,
                query: '',
                totalCount: 0,
                dialogVisible: false,
                applicationId:0,
            }
        },
        mounted(){
            this.loadData()
        },
        methods: {
            loadData() {
                const params={
                    currentPage: this.currentPage,
                    currentPageSize: this.pageSize,
                    query: this.query,
                }
                ApprovalApi.getUnApplicationList(params).then(res=>{
                    this.totalCount = res.data.count
                    this.tableData = res.data.data
                })
            },
            handleSizeChange(val) {
                this.pageSize = val;
                this.loadData();
            },
            handleCurrentChange(val) {
                this.currentPage = val;
                this.loadData();
            },
            //点击查看
            handleLook(id){
                this.applicationId = id
                this.dialogVisible = true
            },
            //点击提交
            handleSubmit(id) {
                this.$confirm('是否确定提交此条申请?', '提示', {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning'
                    }).then(() => {
                        ApprovalApi.submitApplication(id).then(res => {
                            this.loadData()
                            const type1 = res.data.isSuccess? 'success':'error'
                            this.$message({
                                type: type1,
                                message: res.data.message
                            });
                            if(res.data.isSuccess){this.$router.push({ path: '/applicationList'})}                       
                        })
                    }).catch(() => {
                        this.$message({
                            type: 'info',
                            message: '已取消删除'
                        });
                    });
            },
            //点击编辑
            handleEdit(id) {
                this.$router.push({
                    path: "/addApplication/" + id
                });
            },
            handleDelete(id) {
                this.$confirm('此操作将永久删除该申请, 是否继续?', '提示', {
                        confirmButtonText: '确定',
                        cancelButtonText: '取消',
                        type: 'warning'
                    }).then(() => {
                        ApprovalApi.deleteApplication(id).then(res => {
                            this.loadData()
                            const type1 = res.data.isSuccess? 'success':'error'
                            this.$message({
                                type: type1,
                                message: res.data.message
                            });                            
                        })
                    }).catch(() => {
                        this.$message({
                            type: 'info',
                            message: '已取消删除'
                        });
                    });
            },
            //点击搜索
            handleChangeQuery() {
                this.loadData()
            },
            //关闭弹框
            handleClose(){
                this.dialogVisible = false
            },
            closeForm(val){
                this.dialogVisible = val
            },            
        },
        filters: {
            formatDate: function (value) {
                let date = new Date(value);
                let y = date.getFullYear();
                let MM = date.getMonth() + 1;
                MM = MM < 10 ? ('0' + MM) : MM;
                let d = date.getDate();
                d = d < 10 ? ('0' + d) : d;
                return y + '-' + MM + '-' + d;
                }
            }

    }
</script>
<style lang="scss" scoped>
    
</style>

