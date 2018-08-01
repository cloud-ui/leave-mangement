<template>
    <div class="index">
        <div class="index-title">
            <p>待审核申请</p>
        </div>
        <div class="index-body">
            <div class="index-body-title">
                <div style="display:flex;">
                    <p>共 <span>{{totalCount}}</span> 条待审核的申请</p>
                </div>
                <el-input style="width:30%" placeholder="请输入员工名称" v-model="query" class="input-with-select">
                    <el-button slot="append" @click="handleChangeQuery()">搜索</el-button>
                </el-input>
            </div>
            <el-table :data="tableData" :header-cell-style="headerCellStyle" border style="width: 100%">
                <el-table-column align="center" prop="id" label="序号" width="80">
                    <template slot-scope="scope">
                            {{scope.row.id}}
                    </template>
                </el-table-column>
                <el-table-column align="center" prop="workerName" label="员工名称" width="100">
                </el-table-column>
                <el-table-column align="center" prop="type" label="请假类型" width="120">
                </el-table-column>
                <el-table-column align="center" prop="content" label="内容">
                    <template slot-scope="scope">
                        {{scope.row.content}}
                    </template>
                </el-table-column>
                <el-table-column align="center" prop="createTime" label="创建时间" width="110">
                </el-table-column>
                <el-table-column align="center" label="操作" width="80">
                    <template slot-scope="scope">
                        <el-dropdown>
                            <i class="el-icon-menu"></i>
                            <el-dropdown-menu slot="dropdown">
                                <el-dropdown-item>
                                 <el-button type="text" size="small" @click="handleLook(scope.row.id)" icon="el-icon-view">查看</el-button>
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
        <el-dialog title="审核" :visible.sync="dialogVisible" :before-close="handleClose">
         <comp-check @closeForm='handleClose' @close='closeForm' :appId="applicationId" ref="compForm"></comp-check>
        </el-dialog>
    </div>
</template>
<script>
    import '../index.scss'
    import { CheckApi } from "./api.js"
    import CompCheck from './checkapplication'
    export default {
        components:{
            CompCheck
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
                CheckApi.getCheckingList(params).then(res=>{
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
            //点击搜索
            handleChangeQuery() {
                this.loadData();
            },
            //点击查看
            handleLook(id){
                this.applicationId = id
                this.dialogVisible = true                
            },
            //关闭弹框
            handleClose(){
                this.dialogVisible = false
            },
            closeForm(val){
                this.loadData()
                this.dialogVisible = val
                
            }
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
    .link {
        padding: 0px 0px 0px 10px;
        font-size: 14px;
        color: #409eff;
        text-decoration: none;
    }
</style>

