<template>
    <div class="index">
        <div class="index-title">
            <p>公司员工</p>
        </div>
        <div class="index-body">
            <div class="index-body-title">
                <div style="display:flex;">
                    <p>共 <span>{{totalCount}}</span> 名员工</p>
                    <el-button style="padding:0px 0px 0px 10px;" @click="handleAdd()" type="text" icon="el-icon-plus">添加员工</el-button>
                </div>
                <div style="display:flex;">
                    <comp-dep-select style="padding-right:10px;" :depId="depId" @change="changeDep"></comp-dep-select>
                    <el-input placeholder="请输入内容" v-model="query" class="input-with-select">
                        <el-button slot="append" @click="handleChangeQuery()">搜索</el-button>
                    </el-input>
                </div>
            </div>
            <el-table :data="tableData" border stripe :highlight-current-row="true" :header-cell-style="headerCellStyle" style="width: 100%">
                <el-table-column prop="id" label="序号" align="center" width="80"></el-table-column>
                <el-table-column prop="name" width="100" align="center" label="名称"></el-table-column>
                <el-table-column prop="paperNumber" align="center" label="证件号码"></el-table-column>
                <el-table-column prop="deparment" align="center" label="部门"></el-table-column>
                <el-table-column prop="position" width="100" align="center" label="职位"></el-table-column>
                <el-table-column prop="state" width="100" align="center" label="员工状态"></el-table-column>
                <el-table-column prop="entryTime" align="center" label="入职时间">
                    <template slot-scope="scope">
                        {{scope.row.entryTime|formatDate}}
                    </template>
                </el-table-column>
                <el-table-column
                label="操作"
                align="center"
                width="80">
                <template slot-scope="scope">
                    <el-dropdown>
                        <i class="el-icon-menu"></i>
                        <el-dropdown-menu slot="dropdown">
                            <el-dropdown-item>
                             <el-button @click="handleLook(scope.row.id)" type="text" size="small" icon="el-icon-view">查看</el-button>
                            </el-dropdown-item>
                            <el-dropdown-item>
                             <el-button style="color:#5fb878;" type="text" size="small" icon="el-icon-delete">人事调动</el-button>
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
        <el-dialog :title="formTitle" :visible.sync="dialogVisible" :before-close="handleClose">
         <comp-view @closeForm='handleClose' @close='closeForm' :workerId="workerId" :type="type" ref="compForm"></comp-view>
        </el-dialog>
    </div>
</template>
<script>
    import '../dangan.scss'
    import '../../index.scss'
    import {FileApi} from '../api.js'
    import CompDepSelect from '@/packages/components/dep-select'
    import CompView from './operatorview'
    export default {
        data() {
            return {
                tableData: [],
                totalCount: 0,
                depId: 0,
                query: '',
                currentPage: 1,
                pageSize: 20,
                dialogVisible: false,
                formTitle:'',
                workerId:'',
                type:'',
                headerCellStyle: {
                    backgroundColor: '#f2f2f2',
                    fontSize: '14px',
                    color: '#434343',
                    fontWeight: 400
                },
            }
        },
        components: {
            CompDepSelect,
            CompView
        },
        created() {
            this.loadData()
        },
        watch: {
            $route: "loadData"
        },
        methods: {
            handleSizeChange(val) {
                this.pageSize = val;
                this.loadData();
            },
            handleCurrentChange(val) {
                this.currentPage = val;
                this.loadData();
            },
            handleChangeQuery() {
                this.loadData()
            },
            changeDep(val){
                this.depId = val === ""?0:val
                this.loadData()
            },
            //加载表格数据
            loadData() {
                //获取到地址栏传的参数
                const id = parseInt(this.$route.params.depId);
                let params = {
                    currentPage: this.currentPage,
                    currentPageSize: this.pageSize,
                    depId: id != -1 ? id : this.depId,
                    query: this.query
                };
                FileApi.getWorkers(params).then(res => {
                    console.log(res)
                    this.totalCount = res.data.totalCount;
                    this.tableData = res.data.data;
                });
            },
            handleAdd(){
                this.type = 'add'
                this.formTitle = ''
                this.dialogVisible = true
            },
            //点击查看详情
            handleLook(id){
                this.workerId = id
                this.type = 'look'
                this.formTitle = '员工详情'
                this.dialogVisible = true
            },
            //关闭弹窗
            handleClose() {
                this.dialogVisible = false
            },
            closeForm(dialogVisible) {
                this.loadData()
                this.dialogVisible = dialogVisible
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
<style>
    .el-dialog__body {
        padding: 10px 20px;
        color: #606266;
        font-size: 14px;
    }
</style>

