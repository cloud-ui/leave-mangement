<template>
    <div class="index">
        <div class="index-title">
            <p>添加申请</p>
        </div>
        <div class="index-body index-body1">
            <div class="index-body-menu">
                <el-menu :default-active="applyType" class="el-menu-vertical-demo">
                    <el-menu-item index="correction ">
                        <i class="el-icon-tickets"></i>
                        <span slot="title">转正申请</span>
                    </el-menu-item>
                    <el-menu-item index="separation">
                        <i class="el-icon-document"></i>
                        <span slot="title">离职申请</span>
                    </el-menu-item>
                </el-menu>
            </div>
            <el-card style="width:82%" class="box-card">
                <p class="content-title"><span>*</span>申请理由：</p>
                <comp-editor style="height:380px;" ref="apply_editor"></comp-editor>
                <div class="content-btn">
                    <el-button>重置</el-button>
                    <el-button type="primary" :loading="loading" @click="submitData">提交</el-button>
                </div>
            </el-card>
        </div>
    </div>
</template>
<script>
import '../index.scss'
import CompEditor from '../../packages/components/editor'
export default {
    components:{
        CompEditor
    },
    data(){
        return{
            applyType:'',
        }
    },
    created() {
        this.fetchData();
    },
    watch: {
        $route: "fetchData"
    },
    methods:{
        fetchData(){
            const type = this.$route.params.type
            this.applyType = type
        }
    }
}
</script>
<style lang="scss" scoped>
.content-title{
    padding-bottom: 5px;
    font-size: 14px;
    >span{
        color:red;
    }
}
.content-btn{
    display: flex;
    justify-content: flex-end;
    padding-top: 20px;
}
</style>
