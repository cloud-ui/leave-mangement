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
                <el-table-column align="center" prop="title" label="公告名称">
                </el-table-column>
                <el-table-column align="center" prop="type" label="公告类型" width="110">
                </el-table-column>
                <el-table-column align="center" prop="content" label="公告内容">
                    <template slot-scope="scope">
                        <pre  v-html="formatText(scope.row.content)"></pre >
                    </template>
                </el-table-column>
                <el-table-column align="center" prop="createTime" width="110" label="创建时间">
                </el-table-column>
                <el-table-column align="center" prop="createHuman" width="100" label="创建人"></el-table-column>
                <el-table-column align="center" label="操作" width="80">
                    <template slot-scope="scope">
                            <el-dropdown>
                                <i class="el-icon-menu"></i>
                                <el-dropdown-menu slot="dropdown">
                                    <el-dropdown-item>
                                     <el-button type="text" size="small" @click="handleLook(scope.row)" icon="el-icon-view">查看</el-button>
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
        <el-dialog width="408px" :title="noticeData.title" :visible.sync="dialogVisible" :before-close="handleClose">
         <comp-view @closeForm='handleClose' @close='closeForm' :notice="noticeData" ref="compView"></comp-view>
        </el-dialog>
    </div>
</template>
<script>
    import '../index.scss'
    import CompView from '../../packages/components/noticeview'
    import {
        NoticeApi
    } from './api.js'
    export default {
        components: {
            CompView
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
                noticeData: {},
                currentPage: 1,
                pageSize: 20,
                query: '',
                totalCount: 0,
                dialogVisible: false,
            }
        },
        mounted() {
            this.loadData()
        },
        methods: {
            loadData() {
                const params = {
                    currentPage: this.currentPage,
                    currentPageSize: this.pageSize,
                    query: this.query,
                }
                NoticeApi.noticeList(params).then(res => {
                    this.totalCount = res.data.totalCount
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
            handleLook(data) {
                this.noticeData = data
                this.dialogVisible = true
            },
            handleDelete(id) {
                this.$confirm('此操作将删除该公告, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    NoticeApi.deleteNotice(id).then(res => {
                        const type1 = res.data.isSuccess ? 'success' : 'error'
                        this.$message({
                            type: type1,
                            message: res.data.message
                        })
                        this.loadData()
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
            handleClose() {
                this.dialogVisible = false
            },
            closeForm(val) {
                this.loadData()
                this.dialogVisible = val
            },
            formatText(text){
                let num = -1,
                rHtml = new RegExp("\<.*?\>", "ig"), //匹配html标签元素
                aHtml = text.match(rHtml), //存放html元素的数组
                rNbsp = new RegExp("\&.*?\;", "ig"), //匹配&nbsp;
                aNbsp = text.match(rNbsp); //存放&nbsp;元素的数组
                text = text.replace(rHtml, '{~}'); //替换html标签为{~}
                text = text.replace(rNbsp, '{&}'); //替换&nbsp;标签为{&}
                text = text.replace(/{~}/g, function() { //恢复html标签
                    num++;
                    return aHtml[num];
                });
                num = -1;
                text = text.replace(/{&}/g, function() { //恢复&nbsp;标签
                    num++;
                    return aNbsp[num];
                });
                return text;
            }
        },
    }
</script>
