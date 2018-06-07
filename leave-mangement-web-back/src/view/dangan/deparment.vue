<template>
    <div class="comp">
        <div class="comp-title">
            <p>公司部门</p>
        </div>
        <div class="comp-body">
            <div class="comp-body-title">
                <p>共 <span>{{totalCount}}</span> 个部门</p>
                <el-input style="width:200px;" placeholder="请输入内容" v-model="query">
                    <i slot="prefix" class="el-input__icon el-icon-search"></i>
                </el-input>
            </div>
            <el-table :data="tableData" border stripe style="width: 100%">
             <el-table-column
                prop="date"
                label="日期"
                width="180">
             </el-table-column>
             <el-table-column
                prop="name"
                label="姓名"
                width="180">
             </el-table-column>
             <el-table-column
                prop="address"
                label="地址">
                </el-table-column>
            </el-table>
            <el-pagination 
            style="padding-top:10px"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :current-page="currentPage"
            :page-sizes="[100, 200, 300, 400]"
            :page-size="50"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalCount">
            </el-pagination>
        </div>
    </div>
</template>
<script>
import './dangan.scss'
export default {
    data(){
        return{
            tableData:[],
            totalCount:0,
            query:'',
            currentPage:1,
        }
    },
    mounted(){
        this.loadData()
    },
    methods:{
        handleSizeChange(val) {
        console.log(`每页 ${val} 条`);
      },
      handleCurrentChange(val) {
        console.log(`当前页: ${val}`);
      },
      loadData(){

      }
    }
}
</script>
