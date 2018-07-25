<template>
    <!-- 公告管理 -->
<div class="index">
        <div class="index-title">
            <p>公告管理</p>
        </div>
        <div class="index-body">
            <div class="index-body-title">
                <div style="display:flex;">
                    <p>共 <span>{{totalCount}}</span> 条公告</p>
                    <router-link to="/notice/add" class="link"><i class="el-icon-plus"></i>添加公告</router-link>
                </div>
                <el-input style="width:30%" placeholder="请输入公告标题" v-model="query" class="input-with-select">
                    <el-button slot="append" @click="handleChangeQuery()">搜索</el-button>
                </el-input>
            </div>
            <el-table :data="tableData" :header-cell-style="headerCellStyle" border style="width: 100%">
                <el-table-column align="center" prop="id" label="序号" width="80">
                </el-table-column>
                <el-table-column align="center" prop="workerName" label="公告名称" width="100">
                </el-table-column>
                <el-table-column align="center" prop="type" label="公告类型" width="120">
                </el-table-column>
                <el-table-column align="center" prop="startTime" label="公告内容">
                </el-table-column>
                <el-table-column align="center" prop="createTime" label="创建时间">
                </el-table-column>
                <el-table-column align="center" prop="createHuman" label="创建人"></el-table-column>
                <el-table-column align="center" label="操作" width="80">
                    <template slot-scope="scope">
                        <el-dropdown>
                            <i class="el-icon-menu"></i>
                            <el-dropdown-menu slot="dropdown">
                                <el-dropdown-item>
                                 <el-button type="text" size="small" @click="handleLook(scope.row.id)" icon="el-icon-view">查看</el-button>
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
    </div>
</template>
<script>
import '../index.scss'
export default {
    data(){
        return{
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
        }
    },
    methods:{
        loadData(){},
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
    }
}
</script>
