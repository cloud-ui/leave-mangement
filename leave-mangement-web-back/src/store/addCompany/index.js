export default {
    state:{
        step: 'FirstStep',
        companyMessage:{
            name:'',
            address:'',
            corporation:'',
            createTime:'',
            cellPhoneNumber:'',
            email:'',
        },
    },
    mutations: {
        changeStep(state, data) {
            state.step = data
        },
        initStep(state) {
            state.step = 'FirstStep'
        },        
    },
    actions:{
        nextStep({commit},data={}){
            commit('changeStep',data.step)
        },
        backStep({commit},data){
            commit('changeStep',data)
        }
    }
}
