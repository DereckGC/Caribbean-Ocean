import { useNavigate } from "react-router-dom";

interface OfferButtonProps {
    Id: number,
    roomTypeId: number,
}


export default function OfferButton({Id, roomTypeId}: OfferButtonProps){
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(`/booking?id=${Id}&roomTypeId=${roomTypeId}&applyOffers=true`);
    }
    
    return(
        <button className="btn-primary" onClick={handleClick}>Reserve</button>
    )
}