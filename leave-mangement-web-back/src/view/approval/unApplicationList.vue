<template>
    <div class="index">
        <div class="index-title">
            <p>未提交申请</p>
        </div>
        <div class="index-body">
            <div class="index-body-title">
                <div style="display:flex;">
                    <p>共 <span>{{totalCount}}</span> 条未提交的申请</p>
                    <router-link to="/addApplication" class="link"><i class="el-icon-plus"></i>添加申请</router-link>
                </div>
                <el-input style="width:30%" placeholder="请输入内容" v-model="query" class="input-with-select">
                <el-button slot="append" @click="handleChangeQuery()">搜索</el-button></el-input>
            </div>
            <el-table :data="tableData" :header-cell-style="headerCellStyle" border style="width: 100%">
                <el-table-column align="center" prop="id" label="序号" width="80">
                    <template slot-scope="scope">
                        {{scope.row.id}}
                    </template>
                </el-table-column>
                <el-table-column align="center" prop="workerName" label="员工名称" width="180">
                </el-table-column>
                <el-table-column align="center" prop="type" label="请假类型" width="180">
                </el-table-column>
                <el-table-column align="center" prop="startTime" label="开始时间">
                </el-table-column>
                <el-table-column align="center" prop="endTime" label="结束时间">
                </el-table-column>
                <el-table-column align="center" prop="createTime" label="创建时间">
                </el-table-column>
                <el-table-column align="center" label="操作">
                    <template slot-scope="scope">
                        <el-button @click="handleSubmit(scope.row.id)" type="text" size="small" icon="el-icon-upload2">提交</el-button>
                        <el-button @click="handleEdit(scope.row)" type="text" size="small" icon="el-icon-edit">编辑</el-button>
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
                tableData:[{
                    id:1,
                    workerName:'张三',
                    type:'事前-病假',
                    startTime:'2018-06-08',
                    endTime:'2018-06-08',
                    createTime:'2018-02-06'
                }],
                headerCellStyle: {
                    backgroundColor: '#f2f2f2',
                    fontSize: '14px',
                    color: '#434343',
                    fontWeight: 400
                },
                currentPage:1,
                pageSize:20,
                query:'',
                totalCount:0,
                dialogVisible:false
            }
        },
        methods:{
            loadData(){

            },
            handleSizeChange(val) {
                this.pageSize = val;
                this.loadData();
            },
            handleCurrentChange(val) {
                this.currentPage = val;
                this.loadData();
            },
            //点击提交
            handleSubmit(id){
                alert("提交成功")
            },
            //点击编辑
            handleEdit(row){

            },
            //点击搜索
            handleChangeQuery(){

            }
        }
    }
</script>
<style lang="scss" scoped>
.link{
padding:0px 0px 0px 10px;
font-size:14px;
color: #409eff;
text-decoration: none;
}
</style>

