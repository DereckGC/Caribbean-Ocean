import styles from './Booking.module.css'
import { RoomTypeCard } from './RoomTypeCard'
import type { BookingSearchResultProps } from '../types/booking.types';





export const BookingSearchResults = ({ hasSearched, isRecommendation, availableRooms, handleSelectRoom }: BookingSearchResultProps) => {
    return (
        hasSearched && (
            <section className={styles.resultsSection}>
                <div className={styles.resultsContainer}>
                    <div className={styles.resultsHeader}>
                        {isRecommendation ? (
                            <>
                                <h3 className={styles.resultsTitle}>Sorry, we don't have that room available.</h3>
                                <p className={styles.resultsSubtitle}>But we can offer these other beautiful rooms on similar dates:</p>
                            </>
                        ) : (
                            <h3 className={styles.resultsTitle}>Available Rooms</h3>
                        )}
                    </div>
                    {availableRooms.map((room) => (
                        <RoomTypeCard
                            key={room.id}
                            room={room}
                            onSelect={() => handleSelectRoom(room)} // Seleccionamos 
                        />
                    ))}
                </div>
            </section>
        )
    );
};